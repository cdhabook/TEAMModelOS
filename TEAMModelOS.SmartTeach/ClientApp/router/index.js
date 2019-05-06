import Vue from 'vue'
import VueRouter from 'vue-router'
import { routes } from './routes'

Vue.use(VueRouter)

let lang = localStorage.getItem('local');
let router = new VueRouter(
{
    mode: 'history',
    //base: "/" + lang + "/",
  routes
},
)

export default router
