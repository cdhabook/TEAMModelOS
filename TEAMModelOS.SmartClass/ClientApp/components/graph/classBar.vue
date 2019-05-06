<template>
    <div :id="id" style="height: 100%;width:100%;"></div>
</template>

<script>
export default {
    data () {
      return {
        myChart: '',
      }
    },
    props:{
      id:{
        type: String
      },
      todayData:{
        type: Object,
        default: function(){
          return {'一年級': 9, '二年級': 11, '三年級': 16, '四年級':6, '五年級':13, '六年級':22}
        }
      },
      lastWeekData:{
        type: Object,
        default: function(){
          return {'一年級': 5, '二年級': 7, '三年級': 12, '四年級':1, '五年級':2, '六年級':24}
        }
      },
      callBack: {
        type: String
      }
    },
    mounted(){
        this.drawLine();
    },
    methods:{
        drawLine(){
            let _this = this
            // 取得Y軸Label
            let yLabel = Object.keys(this.todayData);
            let todayDataArray = [];
            let lastWeekDataArray = [];

            // 今日資料
            yLabel.map(function(key) {
              todayDataArray.push(_this.todayData[key]);
            });
            // 上週資料
            yLabel.map(function(key) {
              lastWeekDataArray.push(_this.lastWeekData[key]);
            });

            // 基于准备好的dom，初始化echarts实例
            this.myChart = this.$echarts.init(document.getElementById(this.id));
            this.myChart.setOption({
              backgroundColor: '#343a4073',
              tooltip: {
                trigger: 'axis',
                axisPointer: {
                  type: 'shadow'
                },
                // formatter: function(params){
                //   // 故意開啟 但不設定使ToolTip 沒有彈跳的作用 但有Hover 功能
                // }
              },
              grid: {
                left: '5%',
                right: '0',
                bottom: '0',
                top:'0',
                containLabel: true
              },
              xAxis: {
                type: 'value',
                boundaryGap: false,
                axisLabel: {
                    inside: true,
                    textStyle: {
                        color:'transparent'
                    }
                },
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: 'rgba(185, 193, 173, 0.63)'
                    }
                }
              },
              yAxis: {
                type: 'category',
                axisLabel: {
                  textStyle: {
                    fontSize: 12,
                    color: '#94998a'
                  }
                },
                splitLine: {
                  lineStyle: {
                    color: 'rgba(185, 193, 173, 0.63)',
                  },
                  show: true
                },
                axisLine: {
                  lineStyle: {
                    color: 'rgba(185, 193, 173, 0.63)',
                    width: 1,
                  }
                },
                data: yLabel,
              },
              series: [
                {
                  name: 'today',
                  type: 'bar',
                  data: todayDataArray,
                  barWidth: 15, //柱子宽度
                  itemStyle: {
                    normal: {  //渐变色
                      color: new this.$echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: "rgba(28, 208, 161, 1)" // 0% 处的颜色
                      },{
                        offset: 1,
                        color: "rgba(28, 208, 161, 0.5)" // 100% 处的颜色
                      }], false)
                    }
                  }
                },
                {
                  name: 'lastWeek',
                  type: 'bar',
                  data: lastWeekDataArray,
                  barWidth: 15, //柱子宽度
                  itemStyle: {
                    normal: {  //渐变色
                      color: new this.$echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: "rgba(228, 233, 220, 1)" // 0% 处的颜色
                      },{
                        offset: 1,
                        color: "rgba(228, 233, 220, 0.5)" // 100% 处的颜色
                      }], false)
                    }
                  }
                }
              ]
            })
            //highlight觸發項
            this.myChart.on('highlight', function (params) {
              _this.$emit('highLightInfo', {'id': yLabel[params.batch[0].dataIndex], 'today': todayDataArray[params.batch[0].dataIndex], 'lastWeek': lastWeekDataArray[params.batch[1].dataIndex]});            
            });            
            this.myChart.on('downplay', function (params) {            
              _this.$emit('downplay', yLabel[params.batch[0].dataIndex])
            });
        }
    }
}

</script>

<style>
</style>
