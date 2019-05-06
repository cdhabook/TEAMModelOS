<template>
    <div :id="id" style="height: 100%;width:100%;"></div>
</template>

<script>
export default {
    data () {
        return {
            mychat: '',
            realPercent: 100,
            pieFinishPercent: 75,
        }
    },
    props:{
        id:{
            type: String
        },
        pieData:{
            type: Object,
            default: function(){
                return {total:175, value: 128}      
            },
        },
    },
    mounted(){
        this.drawLine();
    },
    created(){
        this.realPercent = Math.round((this.pieData.value / this.pieData.total)* 100);
        this.pieFinishPercent = (this.realPercent / 100.00) * 75
    },
    methods:{
        drawLine(){
            let _this = this
            // 基于准备好的dom，初始化echarts实例
            this.myChart = this.$echarts.init(document.getElementById(this.id));
            this.myChart.setOption({                
                title: {
                    "text": '课堂总数 : ' + _this.pieData.total + '\n' + '完成数量 : ' + _this.pieData.value,
                    "x": '50%',
                    "y": '75%',
                    "textAlign": "center",
                    "textStyle": {
                        "fontWeight": 'lighter',
                        "fontSize": 11,
                        "color": "#A9B2B8",
                    },
                },
                series: [{
                  "name": ' ',
                  "type": 'pie',
                  "radius": ['50%', '68.1%'],
                  "avoidLabelOverlap": false,
                  "startAngle": 225,
                  "color": ["#fff", "transparent"],
                  "hoverAnimation": false,
                  "legendHoverLink": false,
                  "label": {
                      "normal": {
                          "show": false,
                          "position": 'center'
                      },
                      "emphasis": {
                          "show": true,
                          "textStyle": {
                              "fontSize": '30',
                              "fontWeight": 'bold'
                          }
                      }
                  },
                  "labelLine": {
                      "normal": {
                          "show": false
                      }
                  },
                  "data": [{
                      "value": 75,
                      "name": '',
                      "itemStyle": {
                        normal: {
                                color: new this.$echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                offset: 0,
                                color: "rgba(228, 233, 220, 0.14)" // 0% 处的颜色
                            },{
                                offset: 1,
                                color: "rgba(228, 233, 220, 0.14)" // 100% 处的颜色
                            }], false),
                            label: {
                                show: false
                            },
                            labelLine: {
                                show: false
                            }
                        },
                      }
                  }, {
                      "value": 25,
                      "name": '',
                      "itemStyle": {
                          "normal": {
                              color: 'rgba(0,0,0,0)'
                          }
                      }
                  }]
              }, {
                  "name": '',
                  "type": 'pie',
                  "radius": ['50%', '68.1%'],
                  "avoidLabelOverlap": false,
                  "startAngle": 225,
                  "color": ["#fff", "transparent"],
                  "hoverAnimation": false,
                  "legendHoverLink": false,
                  "z": 10,
                  "label": {
                      "normal": {
                          "show": false,
                          "position": 'center'
                      },
                  },
                  "labelLine": {
                      "normal": {
                          "show": false
                      }
                  },
                  "data": [{
                      "name": _this.realPercent.toString(), //完成数实际值
                      "value": _this.pieFinishPercent, // PIE完成数
                      "label": {
                          "normal": {
                              "show": true,
                              "formatter": '{b} %',
                              "textStyle": {
                                  "fontSize": 20,
                                  "fontWeight": "100",
                                  "color": "#ffffff",
                              },
                              "position": "center"
                          }
                      },
                      "itemStyle": {
                        normal: {
                        color: new this.$echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: "rgba(228, 233, 220, 1)" // 0% 处的颜色
                      },{
                        offset: 1,
                        color: "rgba(228, 233, 220, 0.5)" // 100% 处的颜色
                      }], false),
                            label: {
                                show: false
                            },
                            labelLine: {
                                show: false
                            }
                        },
                      }
                  }, {
                      "name": '',
                      "value": (100 - _this.pieFinishPercent), // PIE未完成数 100% 是75
                      "itemStyle": {
                          "normal": {
                              color: 'rgba(0,0,0,0)'
                          }
                      }
                  }]
              }]
            });
        },
    }
}

</script>

<style>
</style>
