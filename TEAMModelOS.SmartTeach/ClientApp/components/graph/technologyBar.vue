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
          return {'计分板': 100, '挑人': 30, '计时器': 28, '抢权': 25, '统计图':24, '即问即答':23, '作品比较':21, '推送':8, '翻牌':6, '二次作答':2}
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
            let barcolor = ['#FF6B6A', '#FF9FF4', '#48DBFC', '#1CD0A1', '#FDC958', '#FFAD76'];
            let _this = this
            // 取得x轴Label
            let xLabel = Object.keys(this.data);
            let courseDataArray = [];
            let max;
            // 资料统整
            xLabel.map(function(key) {
              courseDataArray.push(_this.data[key]);
            });
            
            max = Math.max(...courseDataArray) + 20;

            // 基于准备好的dom，初始化echarts实例
            this.myChart = this.$echarts.init(document.getElementById(this.id));
            this.myChart.setOption({
              title:{
                text:'常用科技分布图',
                left: 'center',
                textStyle:{
                  color: '#fafafa',
                  fontWeight: 100,
                }
              },
              legend: {
                top: 20,
                textStyle: {
                    color: '#fff',
                },
                data: ['总量', '英文', '语文', '英语', '数学', '科学', '综合']
              },
              backgroundColor: '#343a4073',
              tooltip: {
                trigger: 'axis',
                axisPointer: {
                  type: 'shadow'
                },              
              },
              grid: {
                left: '0',
                right: '0',
                bottom: '1px',
                top:'0',
                containLabel: true
              },
              xAxis: {
                type: 'category',
                boundaryGap: true,
                data: xLabel,
                axisLabel: {
                    // inside: true,
                    textStyle: {
                        color:'#fafafa'
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
                  name:'总量',
                  type: 'bar',
                  data: courseDataArray,
                  barWidth: 10, //柱子宽度
                  // barGap: 1, //柱子之间间距
                  itemStyle: {
                    normal: { 
                      color: function(params) {
                        var num=barcolor.length;
                        return barcolor[params.seriesIndex%num]
                      },
                    }
                  }
                },
                {
                  name:'语文',
                  type:'bar',
                  barWidth: 10, //柱子宽度
                  data:[30, 15, 14, 13, 12, 11, 10, 9, 3, 2, 2],
                  itemStyle: {
                    normal: { 
                      color: function(params) {
                        var num=barcolor.length;
                        return barcolor[params.seriesIndex%num]
                      },
                    }
                  }
                },
                {
                  name:'英语',
                  type:'bar',
                  barWidth: 10, //柱子宽度
                  data:[30, 7, 14, 7, 6, 11, 10, 9, 3, 2, 2],
                  itemStyle: {
                    normal: { 
                      color: function(params) {
                        var num=barcolor.length;
                        return barcolor[params.seriesIndex%num]
                      },
                    }
                  }
                },
                {
                  name:'数学',
                  type:'bar',
                  barWidth: 10, //柱子宽度
                  data:[30, 7, 0, 5, 4, 2, 6, 5, 3, 2, 2],
                  itemStyle: {
                    normal: { 
                      color: function(params) {
                        var num=barcolor.length;
                        return barcolor[params.seriesIndex%num]
                      },
                    }
                  }
                },
               {
                  name:'科学',
                  type:'bar',
                  barWidth: 10, //柱子宽度
                  data:[10, 1, 0, 0, 2, 3, 5, 5, 3, 2, 2],
                  itemStyle: {
                    normal: { 
                      color: function(params) {
                        var num=barcolor.length;
                        return barcolor[params.seriesIndex%num]
                      },
                    }
                  }
                },
               {
                  name:'综合',
                  type:'bar',
                  barWidth: 10, //柱子宽度
                  data:[0, 0, 0, 0, 1, 0, 3, 1, 3, 2, 2],
                  itemStyle: {
                    normal: { 
                      color: function(params) {
                        var num=barcolor.length;
                        return barcolor[params.seriesIndex%num]
                      },
                    }
                  }
                },
              ]
            })
        }
    }
}

</script>

<style>
</style>
