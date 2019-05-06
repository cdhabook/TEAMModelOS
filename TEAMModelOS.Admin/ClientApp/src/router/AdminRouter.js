import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/admin/Home.vue'
import Login from '@/views/admin/LoginPage.vue'

Vue.use(Router)

export default new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    routes: [
        {
            path: '/', 
            redirect:'/Login',
        },
        {
            path: '/Login',
            name: 'Login',
            component: Login
        },
        {
            path: '/Home',
            name: 'Home',
            component: Home,
            children: [
                {
                    path: 'RoleManage',
                    name: 'RoleManage',
                    component: () => import('@/views/admin/RoleManage.vue')
                },
                {
                    path: 'UserManage',
                    name: 'UserManage',
                    component: () => import('@/views/admin/UserManage.vue')
                },
                {
                    path: 'ClassEvaluation',
                    name: 'ClassEvaluation',
                    component: () => import('@/views/admin/ClassEvaluation.vue')
                },
                {
                    path: 'DormEvaluation',
                    name: 'DormEvaluation',
                    component: () => import('@/views/admin/DormEvaluation.vue')
                },
                {
                    path: 'StudentEvaluation',
                    name: 'StudentEvaluation',
                    component: () => import('@/views/admin/StudentEvaluation.vue')
                },
                {
                    path: 'SelfEvaluation',
                    name: 'SelfEvaluation',
                    component: () => import('@/views/admin/SelfEvaluation.vue')
                }

            ]
        },
        {
            path: '/ShcoolAccount',
            name: 'ShcoolAccount',
            component: () => import( '@/views/admin/ShcoolAccount.vue')
        }
    ]
})
