import Vue from 'vue'
//import axios from 'axios'
import i18n from '@/locale';
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'
import { FontAwesomeIcon } from './icons'
import iView from 'iview';
import 'iview/dist/styles/iview.css';
import commons from "@/utils/public.js";
import apiTools from '@/api/api.js';
import { fetch, post } from '@/filters/http.js';
import VideoPlayer from 'vue-video-player';
import jwtDecode from 'jwt-decode';

require('video.js/dist/video-js.css');
require('vue-video-player/src/custom-theme.css');
Vue.use(VideoPlayer);

//新添加的
import vuescroll from 'vue-scroll'
import echarts from 'echarts'

//全局API请求
Vue.prototype.$api = apiTools;
Vue.prototype.$post = post;
Vue.prototype.$get = fetch;


Vue.prototype.$jwtDecode = jwtDecode;

Vue.use(vuescroll)
Vue.prototype.$echarts = echarts

//ZXJ
Vue.prototype.common = commons;

// Registration of global components
Vue.component('icon', FontAwesomeIcon);

//使用钩子函数对路由进行权限跳转
//router.beforeEach((to, from, next) => {
//const role = localStorage.getItem('token');
//  if (!role && to.path !== '/') {
//    next('/');
//  } else {
//    next();
//  }
//})
//Vue.prototype.$http = axios

Vue.use(iView, {
  i18n: (key, value) => i18n.t(key, value)
})

sync(store, router)

const app = new Vue({
  store,
  router,
  i18n,
  ...App
})

export {
  app,
  router,
  store
}
