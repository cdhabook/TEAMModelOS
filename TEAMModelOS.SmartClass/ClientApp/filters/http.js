import axios from 'axios';
import Vue from 'vue'
import router from '@/router/index'
axios.defaults.timeout = 10000; //设置超时时长
axios.defaults.baseURL ='';

//http request 拦截器
axios.interceptors.request.use(
  config => {
    // const token = getCookie('名称');
    config.data = JSON.stringify(config.data);

    if (localStorage.getItem('token')) {
      config.headers = {
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + localStorage.getItem('token'),
        'lang': localStorage.getItem('local')
      }
    } else {
      config.headers = {
        'Content-Type': 'application/json',
        'Authorization': ""
      }
    }
    return config;
  },
  error => {
    return Promise.reject(err);
  }
);


//http response 拦截器
axios.interceptors.response.use(
  response => {
    if (response.data.errCode == 2) {
      router.push({
        path: "/login",
        querry: { redirect: router.currentRoute.fullPath }//从哪个页面跳转
      })
    }
    return response;
  },
  error => {
    if (401 === error.response.status) {
      localStorage.clear();
      console.log(window.location);
      window.location.href = window.location.origin;
      alert("登录状态已过期！请重新登录！");
    } else if (500 === error.response.status) {
      //alert("服务器错误！");
    } else {
      return Promise.reject(error);
    }
  }
)


/**
 * 封装get方法
 * @param url
 * @param data
 * @returns {Promise}
 */

export function fetch(url, params) {
  let data = {};
  data.method = url;
  data.params = params;
  data.lang = localStorage.getItem('local');
  return new Promise((resolve,reject) => {
    axios.get(url, data)
      .then(response => {
        resolve(response.data);
      //  this.$Message.success('数据访问成功！');
      })
      .catch(err => {
        reject(err);
        this.$Message.error('数据访问错误！');
      })
  })
}

/**
 * 封装post请求
 * @param url
 * @param data
 * @returns {Promise}
 */

export function post(url, params) {
  let data = {};
  data.method = url;
  data.params = params;
  data.lang = localStorage.getItem('local');
  return new Promise((resolve,reject) => {
    axios.post(url,data)
      .then(response => {
        resolve(response.data);
       // this.$Message.success('数据访问成功！');
      },err => {
        reject(err);
        //this.$Message.error('数据访问错误！');
      })
  })
}
