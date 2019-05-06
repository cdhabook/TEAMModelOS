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
          return {'挑人': 9, '计时器': 11, '计分板': 16, 'I R S':14, '即问即答':9, '二次作答':5, '翻牌':3, '统计图':10, 
                  '抢权':11, '飞讯处理':3, '推送':6, 'L I V E':13, '微影片':7, '播放影片':10, '作品观摩':7, '作品点评':6}
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
            let yLabel = Object.keys(this.data);
            let todayDataArray = [];

            // 今日資料
            yLabel.map(function(key) {
              todayDataArray.push(_this.data[key]);
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
                formatter: function(params){
                  // 故意開啟 但不設定使ToolTip 沒有彈跳的作用 但有Hover 功能
                }
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
                      fontSize: '13px',
                      color:'#94998a'
                    }
                },
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: 'rgba(185, 193, 173, 0.63)'
                    }
                },
                data: yLabel,
              },
              series: [
                {
                  // name: 'today',
                  type: 'bar',
                  data: todayDataArray,
                  barWidth: 7, //柱子宽度
                  barGap: 1, //柱子之间间距
                  itemStyle: {
                    normal: {  //渐变色
                      color: new this.$echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: "rgba(228, 233, 220, 1)" // 0% 处的颜色
                      },{
                        offset: 1,
                        color: "rgba(228, 233, 220, 0)" // 100% 处的颜色
                      }], false)
                    }
                  }
                }
              ]
            })
            // //highlight觸發項
            // this.myChart.on('highlight', function (params) {
            //   _this.$emit('highLightInfo', {'id': yLabel[params.batch[0].dataIndex], 'today': todayDataArray[params.batch[0].dataIndex], 'lastWeek': lastWeekDataArray[params.batch[1].dataIndex]});            
            // });            
            // this.myChart.on('downplay', function (params) {            
            //   _this.$emit('downplay', yLabel[params.batch[0].dataIndex])
            // });
            // //預設顯示第一個
            // this.myChart.dispatchAction({
            //     type: 'showTip',
            //     seriesIndex: 1,
            //     dataIndex: 0,
            // });
            // // 預設先給值
            // this.$emit('highLightInfo', {'id': yLabel[0], 'today': todayDataArray[0], 'lastWeek': lastWeekDataArray[0]});
        }
    }
}

</script>

<style>
</style>
