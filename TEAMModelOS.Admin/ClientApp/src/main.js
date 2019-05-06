import Vue from 'vue'
import App from './App.vue'
import router from './router/AdminRouter'
import store from './store'
import iView from 'iview'
import i18n from './i18n'
import 'iview/dist/styles/iview.css';
import api from './api.js'
import animate from 'animate.css'

Vue.use(animate);
Vue.prototype.$api = api;
Vue.config.productionTip = false
Vue.use(iView);
new Vue({
    router,
    store,
    i18n,
    render: h => h(App)
}).$mount('#app')
