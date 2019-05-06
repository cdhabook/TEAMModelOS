import Login from '@/view/login'
import ServerSideLogin from '@/view/serverside/login'
// import HTTP404 from '@/view/404'
import Index from '@/view/index'
import { resolve } from 'url';

export const routes = [
  { name: 'login', path: '/login', component: Login },
  { name: 'ServerSideLogin', path: '/serverside/login', component: ServerSideLogin },
  { name: 'SmartClassDashBoard', path: '/smartclassdashboard', component: resolve => require(['@/view/smart-dashboard/class/smart-class-dashboard'], resolve), },
  { name: 'SmartIOTDashBoard', path: '/smartiotdashboard', component: resolve => require(['@/view/smart-dashboard/IOT/smart-iot-dashboard'], resolve), },
  { name: 'SmartTALDashBoard', path: '/smarttaldashboard', component: resolve => require(['@/view/smart-dashboard/TAL/smart-tal-dashboard'], resolve), },
  { name: 'index', path: '', component: Index },
  //学情分析
  {
    name: 'saindex',
    path: '/saindex',
    component: resolve => require(['@/view/student-analysis/index/SAindex.vue'], resolve),
  },
  {
    name: 'teach',
    path: '/teach',
    component: resolve => require(['@/view/student-analysis/index/SAteach.vue'], resolve),
  },
  {
    name:'total',
    path: '/total',
    component: resolve => require(['@/view/student-analysis/total-analysis/SAtotal.vue'],resolve),
  },
  // { name: 'HTTP404', path: '/*', component: HTTP404, display: 'Home', icon: 'home' },


  {
    path: '/syllabus',
    component: resolve => require(['@/view/syllabus/index/index.vue'], resolve), //路由懒加载
  }
]
