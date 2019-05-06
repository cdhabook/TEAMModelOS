<template>
    <div class="login">
        <div class="drag">
            <div style="background:rgba(255,255,255,0.1); border-radius:5px;" >
                <Card icon="log-in" title="欢迎登录" :bordered="false" class="login-card">
                    <div class="form-con">
                        <Form ref="loginForm" :model="form" :rules="rules" @keydown.enter.native="handleSubmit">
                            <FormItem prop="userName">
                                <Input v-model="form.userName" placeholder="请输入用户名">
                                <span slot="prepend">
                                    <Icon :size="16" type="ios-person"></Icon>
                                </span>
                                </Input>
                            </FormItem>
                            <FormItem prop="password">
                                <Input type="password" v-model="form.password" placeholder="请输入密码">
                                <span slot="prepend">
                                    <Icon :size="14" type="md-lock"></Icon>
                                </span>
                                </Input>
                            </FormItem>
                            <FormItem>
                                <Button @click="handleSubmit" type="primary" long>登录</Button>
                            </FormItem>
                        </Form>
                        <p class="login-tip">输入任意用户名和密码即可</p>
                    </div>
                </Card>
            </div>
        </div>
        
        
    </div>
</template>
<script>
    export default {
        name: 'LoginForm',
        props: {
            userNameRules: {
                type: Array,
                default: () => {
                    return [
                        { required: true, message: '账号不能为空', trigger: 'blur' }
                    ]
                }
            },
            passwordRules: {
                type: Array,
                default: () => {
                    return [
                        { required: true, message: '密码不能为空', trigger: 'blur' }
                    ]
                }
            }
        },
        data() {
            return {
                form: {
                    userName: 'super_admin',
                    password: ''
                }
            }
        },
        computed: {
            rules() {
                return {
                    userName: this.userNameRules,
                    password: this.passwordRules
                }
            }
        },
        methods: {
            handleSubmit() {
                this.$refs.loginForm.validate((valid) => {
                    if (valid) {
                        if (this.password == 'habook') {
                            this.$router.push({ path: '/home' });
                        } else {
                            this.$router.push({ path: '/home' });
                        }
                    }
                })
            }
        }
    }
</script>
<style scoped>
    .login {
        background-image:url('../../assets/img/login_bg.jpg');
        width:100%;
        height:100%;
        background-size:cover;
        display:flex;
        flex-direction:row;
        align-items:center;
        justify-content:center;
    }
        .login:after {
            content: "";
            width: 100%;
            height: 100%;
            position: absolute;
            left: 0;
            top: 0;
            background: inherit;
            filter: blur(4px);
            z-index: 1;
        }
    .drag {
        position: absolute;
        text-align: center;
        z-index: 11;
        border-radius: 5px;
        box-shadow: 0 0 10px 6px rgba(0,0,0,.5);
        width: 90%;
        max-width: 350px;
    }
    .login-tip {
        color:white;
    }
    .drag >>> .ivu-card-head p span {
        color:white;
        font-size:18px;
    }
    .login-card {
        background: none;
        color: white;
        width:100%;
    }
</style>