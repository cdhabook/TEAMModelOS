<template>
  <div id="app">
    <div class="main-content" v-show="preFlag">
      <img src="../assets/tmd_logo.png" class="logo animated fadeIn" @load="loadImage" />

      <!-- 选择身份 -->
      <div class="centerCol" v-if="isSelectRole">
        <span class="title">{{$t('index.chooseRole')}}</span>
        <div class="select-role-wrap center">
          <div class="role-item centerCol" v-for="(item,index) in roleInfo" @click="handleRole(item,index)">
            <Icon type="ios-ribbon" size="60" /><span class="role-name">{{item.role.name}}</span>
          </div>
        </div>
      </div>

      <!-- 选择模块 -->
      <div class="auth-wrap centerCol" v-else>
        <Button type="primary" size="large" class="btn-login animated fadeIn" @click="handleLogin('login')" v-show="!isLogin">{{$t('index.login')}}</Button>
        <Button type="primary" size="large" class="btn-login animated fadeIn" @click="handleLogin('regist')" v-show="!isLogin">{{$t('index.register')}}</Button>
        <!--<Button type="primary" size="large" class="btn-login animated fadeIn" @click="handleAuth()" v-show="!hasAuthSchool">去授权</Button>
        <p v-show="isLogin" class="suc-text">{{$t('index.authSchool')}}: {{schoolInfo.schoolName}}</p>-->
        <div v-show="isLogin" class="suc-text select-role">
          <span>{{$t('index.currentRole')}}：</span>
          <Select v-model="currentRole" style="width:200px" @on-change="handleRoleChange">
            <Option v-for="(item,index) in roleInfo" :value="index" :key="item.role.name">{{ item.role.name }}</Option>
          </Select>
        </div>
        <div v-show="isLogin" class="overlay-wrap center animated fadeIn">
          <SelectModule :moduleList="moduleList"></SelectModule>
        </div>
      </div>

    </div>
    <!-- 用户信息 -->
    <div class="user-wrap" v-show="isLogin">
      <div>
        <span class="ivu-avatar ivu-avatar-circle ivu-avatar-default ivu-avatar-icon"><i class="ivu-icon ivu-icon-ios-person"></i></span>
        <span>{{username}}</span>
        <span class="btn-exit" @click="handleExit">{{$t('index.exit')}}</span>
      </div>
    </div>
  </div>
</template>
<script>
  import animate from 'animate.css'
  import SelectModule from '@/components/syllabus/SelectModule.vue'
  export default {
    components: {
      SelectModule
    },
    data() {
      return {
        isLogin: false,
        isSelectRole: false,
        username: "",
        hasAuthSchool: true,
        schoolInfo: {},
        preFlag: false,
        currentRole: "",
        roleInfo:[],
        moduleList: [{
          name: "学情分析",
          link: "/saindex"
        }, {
          name: "课纲管理",
          link: "/syllabus"
        }]
      }
    },
    methods: {

      //前往授权
      handleAuth() {
        this.$router.replace('/authorization')
      },
      //图片加载完成后再展示
      loadImage() {
        this.preFlag = true;
      },
      //访问TW登录并回调
      handleLogin(types) {
        let that = this;
        that.$api.getLoginLink({ date: "151615156", type: types }).then(res => {
          let loginUrl = res.result.data;
          let callback = window.location.href;
          window.location.href = loginUrl + callback;
        })
      },
      //获取URL地址的指定参数
      getParamAsCH(paramName) {
        var paramValue = "";
        var isFound = false;
        if (window.location.search.indexOf("?") == 0 && window.location.search.indexOf("=") > 1) {
          var arrSource = decodeURI(window.location.search).substring(1, window.location.search.length).split("&");
          var i = 0;
          while (i < arrSource.length && !isFound) {
            if (arrSource[i].indexOf("=") > 0) {
              if (arrSource[i].split("=")[0].toLowerCase() == paramName.toLowerCase()) {
                paramValue = arrSource[i].split("=")[1];
                isFound = true;
              }
            }
            i++;
          }
        }
        paramValue = unescape(paramValue)
        return paramValue;
      },
      //登录回调函数获取参数
      loginCallback() {
        let that = this;
        let bcrypt = require('bcryptjs');
        let name = this.getParamAsCH("name") ? this.getParamAsCH("name") : localStorage.getItem('username');
        let teamModelId = this.getParamAsCH("id");
        let ticket = this.getParamAsCH("ticket");
        let token = localStorage.getItem('token') ? localStorage.getItem('token') : "";
        that.$Spin.show();
        that.$api.checkLogin({  //登录验证
          name: name,
          teamModelId: teamModelId,
          ticket: ticket,
          token: token,
          sign: bcrypt.hashSync(ticket + teamModelId)
        }).then(res => {
          let token = res.result.data.jwtToken.access_token;
          if (token) {
            if (!localStorage.getItem('token')) {
              that.$Message.success(this.$t('index.loginSuc'));
            }
            that.getLoginClaim();//获取身份信息
            let decode = that.$jwtDecode(res.result.data.jwtToken.access_token);//解析返回的token
            localStorage.setItem('token', token);
            localStorage.setItem('username', decode.name);
            localStorage.setItem('decodeData', JSON.stringify(decode));
            that.username = decode.name;
            that.isLogin = true;
            that.isSelectRole = true;
            that.$Spin.hide();
          } else {
            that.$Message.warning("服务器错误！未返回token");
          }
        }).catch(res => {
          //that.$Message.warning("当前未登录");
          that.$Spin.hide();
        })
      },

      //获取登录用户绑定学校身份信息
      getLoginClaim() {
        this.$api.getLoginClaim({}).then(res => {
          this.roleInfo = res.result.data;
        })
      },

      //选择身份跳转到模块选择
      handleRole(role, index) {
        localStorage.setItem('c_role_info', JSON.stringify(role));
        for (let i in role.roleClaim) {
          if (role.roleClaim[i].claim) {
            let str = " ";
            let roleClaim = role.roleClaim[i].claim;
            for (let j in roleClaim) {
              str = str + roleClaim[j].claimName
            }
            console.log(str);
          }
        }
        this.isSelectRole = false;
        this.currentRole = this.roleInfo.indexOf(role);
      },
      //身份变化
      handleRoleChange(val) {
        let role = this.roleInfo[val];
        localStorage.setItem('c_role_info', JSON.stringify(this.roleInfo[val]));
        for (let i in role.roleClaim) {
          if (role.roleClaim[i].claim) {
            let str = " ";
            let roleClaim = role.roleClaim[i].claim;
            for (let j in roleClaim) {
              str = str + roleClaim[j].claimName
            }
            console.log(str);
          }
        }
      },

      //退出操作清除存储数据
      handleExit() {
        localStorage.clear();
        window.location.href = window.location.origin;
      },

    },
    mounted() {
      this.loginCallback();
    }
  }
</script>
<style scoped>
  html, body, #app {
    height: 100% !important;
    /*-moz-user-select: none; /*火狐 firefox*/
    /*-webkit-user-select: none;/ /*webkit浏览器*/
    /*-ms-user-select: none;*/ /*IE10+*/
    /*user-select: none;*/
  }

  .main-content {
    position: relative;
    width: 100%;
    min-width: 1200px;
    min-height: 768px;
    background: url("http://chq.dygl.pujiaoyun.cn/static/img/banner.071530a.jpg") center 100% no-repeat;
    height: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    padding-top: 50px;
  }

  .center {
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
  }

  .centerCol {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }

  .overlay-wrap {
    padding: 20px 10px;
    background-color: rgba(218, 218, 218, 0.08);
    margin-top: 50px;
  }

  .logo {
    width: 200px;
    height: 200px;
    margin-bottom: 50px;
  }

  .btn-login {
    width: 150px;
    margin-top: 15px;
    background-color: rgba(204,204,204,.37) !important;
    border-color: #87888a !important;
  }

  .suc-text {
    color: #fff;
    font-size: 14px;
    font-weight: 200;
  }

  .user-wrap {
    position: absolute;
    right: 50px;
    top: 50px;
    color: white;
    font-size: 14px;
    font-weight: 200;
  }

  .ivu-avatar {
    background-color: #30a6e1 !important;
    margin-right: 10px;
  }

  .btn-exit {
    font-size: 12px;
    cursor: pointer;
    margin-left: 15px;
  }

  .select-role {
    margin-top: 15px;
  }

    .select-role /deep/ .ivu-select {
      color: #000 !important;
      width: 150px !important;
    }

    .select-role .ivu-select-selection {
      background-color: rgba(255,255,255,.18) !important;
      border-color: #373737 !important;
    }

  .select-role-wrap {
    background-color: rgba(218, 218, 218, 0.08);
    padding: 5px;
    justify-content: normal;
    flex-wrap: wrap;
    max-width: 930px;
  }

  .role-item {
    width: 210px;
    height: 210px;
    background-color: rgba(179,179,179,.15);
    margin: 10px;
    color: #c5c5c5;
    font-size: 14px;
    font-weight: 200;
    cursor: pointer;
  }

    .role-item:hover {
      background: rgba(179,179,179,.42);
      color: #fff;
    }

  .title {
    font-size: 26px;
    font-weight: 200;
    color: #fff;
    margin-bottom: 20px;
  }

  .role-name {
    margin-top: 10px;
  }
</style>
