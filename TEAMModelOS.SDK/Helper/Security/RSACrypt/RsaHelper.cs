using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TEAMModelOS.SDK.Helper.Common.JsonHelper;

namespace TEAMModelOS.SDK.Helper.Security.RSACrypt
{
    public static class RsaHelper
    {
        public static string RSASign(string data, string privateKeyPem)
        {
            RSACryptoServiceProvider rsaCsp = LoadCertificateFile(privateKeyPem);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] signatureBytes = rsaCsp.SignData(dataBytes, "SHA1");
            return Convert.ToBase64String(signatureBytes);
        }

        private static byte[] GetPem(string type, byte[] data)
        {
            string pem = Encoding.UTF8.GetString(data);
            string header = String.Format("-----BEGIN {0}-----\\n", type);
            string footer = String.Format("-----END {0}-----", type);
            int start = pem.IndexOf(header) + header.Length;
            int end = pem.IndexOf(footer, start);
            string base64 = pem.Substring(start, (end - start));
            return Convert.FromBase64String(base64);
        }
        public static string LoadCertificateFileToSting(string filename)
        {
            FileStream fs = System.IO.File.OpenRead(filename);
            byte[] data = new byte[fs.Length];
            byte[] res = null;
            fs.Read(data, 0, data.Length);
            if (data[0] != 0x30)
            {
                res = GetPem("RSA PRIVATE KEY", data);
            }
            return res.ToJson();
        }
        public static RSACryptoServiceProvider LoadCertificateFile(string filename)
        {
            FileStream fs = System.IO.File.OpenRead(filename);
            byte[] data = new byte[fs.Length];
            byte[] res = null;
            fs.Read(data, 0, data.Length);
            if (data[0] != 0x30)
            {
                res = GetPem("RSA PRIVATE KEY", data);
            }
            string ss = res.ToJson();
            RSACryptoServiceProvider rsa = DecodeRSAPrivateKey(res);
            return rsa;
        }

        private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // --------- Set up stream to decode the asn.1 encoded RSA private key ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);  //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();    //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------ all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);
                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                CspParameters CspParameters = new CspParameters
                {
                    Flags = CspProviderFlags.UseMachineKeyStore
                };
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024, CspParameters);
                RSAParameters RSAparams = new RSAParameters
                {
                    Modulus = MODULUS,
                    Exponent = E,
                    D = D,
                    P = P,
                    Q = Q,
                    DP = DP,
                    DQ = DQ,
                    InverseQ = IQ
                };
                RSA.ImportParameters(RSAparams);
                return RSA;
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
            finally
            {
                binr.Close();
            }
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)        //expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();    // data size in next byte
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte();    // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;        // we already have the data size
            }

            while (binr.ReadByte() == 0x00)
            {    //remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);        //last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }
    }
}
