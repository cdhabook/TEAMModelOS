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
      data:{
        type: Object,
        default: function(){
          return {'模擬測驗': 0, '成績登錄': 5, '線上測驗': 16, 'HiTeach': 150, '合併活動':6, '班級競賽':23, '網路閱卷':21}
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
            // 取得x軸Label
            let xLabel = Object.keys(this.data);
            let courseDataArray = [];
            let max;
            // 資料統整
            xLabel.map(function(key) {
              courseDataArray.push(_this.data[key]);
            });
            
            max = Math.max(...courseDataArray) + 20;

            // 基于准备好的dom，初始化echarts实例
            this.myChart = this.$echarts.init(document.getElementById(this.id));
            this.myChart.setOption({
              backgroundColor: '#343a4073',
              tooltip: {
                trigger: 'axis',
                axisPointer: {
                  type: 'shadow'
                },
                formatter: function(params){
                  // 故意開啟 但不設定使ToolTip 沒有彈跳的作用 但有Hover 功能
                }
              },
              grid: {
                left: '0',
                right: '2%',
                bottom: '1px',
                top:'0',
                containLabel: true
              },
              xAxis: {
                type: 'category',
                boundaryGap: false,
                data: xLabel,
                boundaryGap: ['20%', '20%'],
                axisLabel: {
                    inside: true,
                    textStyle: {
                        color:'transparent'
                    }
                },
                axisLine: {
                  show: true,
                    lineStyle: {
                      width: 1,
                      color: 'rgba(185, 193, 173, 0.63)'
                    }                  
                },
                splitLine: {
                    show: true,
                    lineStyle: {
                      width: 1,
                      color: 'rgba(185, 193, 173, 0.63)'
                    }
                }
              },
              yAxis: {
                type: 'value',
                max: max,
                minInterval: 1,
                axisLabel: {
                  inside: true,
                  textStyle: {
                      color:'transparent'
                  }
                },
                axisLine: {
                  show: true,
                    lineStyle: {
                      width: 1,
                      color: 'rgba(185, 193, 173, 0.63)'
                    }                  
                },
                splitLine: {
                    show: true,
                    lineStyle: {
                      width: 1,
                      color: 'rgba(185, 193, 173, 0.63)'
                    }
                },
              },
              series: [
                {
                  // name: 'today',
                  type: 'bar',
                  data: courseDataArray,
                  barWidth: 10, //柱子宽度
                  barGap: 1, //柱子之间间距
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
        }
    }
}

</script>

<style>
</style>
