import Vue from 'vue'
import VueI18n from 'vue-i18n'
Vue.use(VueI18n)

// ����Ϊ���԰��������õĳ�������������ʱ���԰��赥������
const messages = {
    'zh-CN': require('./assets/i18n/zh-cn/zh-cn'),   // ���ĺ��w���԰�
    'en': require('./assets/i18n/en-us/en-us'),    // Ӣ�����԰�
    'zh-TW': require('./assets/i18n/zh-tw/zh-tw')    // ���ķ��w
}

export default new VueI18n({
    locale: 'zh-CN', // set locale Ĭ����ʾӢ��
    messages: messages // set locale messages
})
