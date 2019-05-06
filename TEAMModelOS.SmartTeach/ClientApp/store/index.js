import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

// TYPES
const MAIN_SET_COUNTER = 'MAIN_SET_COUNTER'

// STATE
//const state = {
//  counter: 1
//}


// MUTATIONS
const mutations = {
  [MAIN_SET_COUNTER](state, obj) {
    state.counter = obj.counter
  }
}

// ACTIONS
const actions = ({
  setCounter({ commit }, obj) {
    commit(MAIN_SET_COUNTER, obj)
  }
})

export default new Vuex.Store({
  //state,
  mutations,
  actions,
  state: {
    //选择关注年级
    selectgrade: [],
    //选择关注学期
    selectterm: [],
    //选择某次考试
    selectexam: {},
    //基础数据
    basicsdata: {},
    //柱状图
    zhuxhuang: {},
    //雷达图
    leida: {},
    //饼图
    pie: {},
     //折线图（大）
    linechart: {},
    //考试类型选择
    examtype: {},

    //文理科展现
    wenli_show: true,

    //任教老师页面
    //选择数据对比
    selectcontrast: [],
    //柱状图
    barline: {},
    //大饼图
    annulus: {},
    //PR
    accuracyPR: {},

    periodList: [{
      value: '1',
      label: '1',
      children: [
        {
          value: '1-1',
          label: '1-1',
          children: [
            {
              value: '1-1-1',
              label: '1-1-1',
              children: [
                {
                  value: '1-1-1-1',
                  label: '1-1-1-1',
                }
              ]
            },
            {
              value: '1-1-2',
              label: '1-1-2',
              children: [
                {
                  value: '1-1-2-1',
                  label: '1-1-2-1',
                }
              ]
            }
          ]
        },
        {
          value: '1-2',
          label: '1-2',
          children: [
            {
              value: '1-2-1',
              label: '1-2-1',
              children: [
                {
                  value: '1-2-1-1',
                  label: '1-2-1-1',
                }
              ]
            },
            {
              value: '1-2-2',
              label: '1-2-2',
              children: [
                {
                  value: '1-2-2-1',
                  label: '1-2-2-1',
                }
              ]
            }
          ]
        },

      ]
    }, {
      value: '2',
      label: '2',
      children: [
        {
          value: '2-1',
          label: '2-1',
          children: [
            {
              value: '2-1-1',
              label: '2-1-1',
              children: [
                {
                  value: '2-1-1-1',
                  label: '2-1-1-1',
                }]
            },
            {
              value: '2-1-2',
              label: '2-1-2',
              children: [
                {
                  value: '2-1-2-1',
                  label: '2-1-2-1'
                },
                {
                  value: '2-1-2-2',
                  label: '2-1-2-2'
                }
              ]
            },
          ]
        },
        {
          value: '2-2',
          label: '2-2',
          children: [
            {
              value: '2-2-1',
              label: '2-2-1',
              children: [
                {
                  value: '2-2-1-1',
                  label: '2-2-1-1',
                }]
            },
            {
              value: '2-2-2',
              label: '2-2-2',
              children: [
                {
                  value: '2-2-2-1',
                  label: '2-2-2-1'
                },
                {
                  value: '2-2-2-2',
                  label: '2-2-2-2'
                }
              ]
            },
          ]
        },

      ]
    }]
  },
})
