import Vue from 'vue'
import VueI18n from 'vue-i18n'
import customZhCn from './lang/zh-CN'
import customZhTw from './lang/zh-TW'
import customEnUs from './lang/en-US'

Vue.use(VueI18n)

// 自动根据浏览器系统语言设置语言
const navLang = navigator.language
const localLang = (navLang === 'zh-TW' || navLang === 'zh-CN' || navLang === 'en-US') ? navLang : false
let lang = localLang || 'en-US'

console.log(lang);
localStorage.setItem('local', lang);

Vue.config.lang = lang

Vue.locale = () => { }
const messages = {
  'zh-CN': customZhCn,
  'zh-TW': customZhTw,
  'en-US': customEnUs
}
const i18n = new VueI18n({
  locale: lang,
  messages
})

export default i18n
