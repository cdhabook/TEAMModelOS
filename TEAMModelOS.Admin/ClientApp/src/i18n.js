import Vue from 'vue'
import VueI18n from 'vue-i18n'
Vue.use(VueI18n)

// 以下为语言包单独设置的场景，单独设置时语言包需单独引入
const messages = {
    'zh-CN': require('./assets/i18n/zh-cn/zh-cn'),   // 中文簡體语言包
    'en': require('./assets/i18n/en-us/en-us'),    // 英文语言包
    'zh-TW': require('./assets/i18n/zh-tw/zh-tw')    // 中文繁體
}

export default new VueI18n({
    locale: 'zh-CN', // set locale 默认显示英文
    messages: messages // set locale messages
})
