<template>
  <div class="barline_box">
    <div id="barline" style="width:100%; height:450px;"></div>
  </div>
</template>
<script>
  export default {
    data() {
      return {
        now_url: this.$route.path,
      }
    },
    created() {
      this.init();
    },
    computed: {
      barline() {
        return this.$store.state.barline
      }
    },
    methods: {
      drawLine(obj) {
        let myChart = this.$echarts.init(document.getElementById('barline'));
        let _this = this;
        myChart.setOption({
          title: {
            text: obj.titlename,
            top: "0%",
          },
          tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
              type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            }
          },
          legend: {
            data: obj.titledata,
            x: 'center',
            top: "3%"
          },
          grid: {
            left: '1.5%',
            right: '4%',
            bottom: '4.5%',
            containLabel: true,
            x: 100,
            y2: 200,
          },
          toolbox: {
            show: true,
            orient: 'horizontal',
            y:"2%",
            feature: {
              mark: { show: true },
              dataView: { show: true, readOnly: false },
              magicType: { show: true, type: ['bar', 'line',] },
              restore: { show: false },
              saveAsImage: { show: true }
            }
          },
          calculable: true,
          xAxis: [
            {
              type: 'category',
              boundaryGap: true,
              data: obj.object_name,
              axisLabel: {
                interval: 0,    //强制文字产生间隔
                rotate: 50,     //文字逆时针旋转50°
                textStyle: {    //文字样式
                  color: "#333",
                  fontSize: 11,
                  fontFamily: 'Microsoft YaHei',
                }
              },
            }
          ],
          dataZoom: {
            show: true,
            realtime: true,
            backgroundColor: "rgba(0,0,0,0)",
            handleSize: 15,
            backgroundColor: '#fff',
            showDataShadow: false,
            handleIcon: 'M-292,322.2c-3.2,0-6.4-0.6-9.3-1.9c-2.9-1.2-5.4-2.9-7.6-5.1s-3.9-4.8-5.1-7.6c-1.3-3-1.9-6.1-1.9-9.3c0-3.2,0.6-6.4,1.9-9.3c1.2-2.9,2.9-5.4,5.1-7.6s4.8-3.9,7.6-5.1c3-1.3,6.1-1.9,9.3-1.9c3.2,0,6.4,0.6,9.3,1.9c2.9,1.2,5.4,2.9,7.6,5.1s3.9,4.8,5.1,7.6c1.3,3,1.9,6.1,1.9,9.3c0,3.2-0.6,6.4-1.9,9.3c-1.2,2.9-2.9,5.4-5.1,7.6s-4.8,3.9-7.6,5.1C-285.6,321.5-288.8,322.2-292,322.2z',
            fillerColor: new this.$echarts.graphic.LinearGradient(1, 0, 0, 0, [{
              //给颜色设置渐变色 前面4个参数，给第一个设置1，第四个设置0 ，就是水平渐变
              //给第一个设置0，第四个设置1，就是垂直渐变
              offset: 0,
              color: '#1eb5e5'
            }, {
              offset: 1,
              color: '#5ccbb1'
            }]),
            handleColor: '#ddd',//h滑动图标的颜色
            handleStyle: {
              borderColor: "#cacaca",
              borderWidth: "1",
              shadowBlur: 0,
              background: "#ddd",
              shadowColor: "#ddd",
            },
            backgroundColor: '#eee',//两边未选中的滑动条区域的颜色
            y: 420,
            height: 8,
            start: 0,
            end: 45,
          },
          yAxis: [
            {
              type: 'value'
            }
          ],
          series: [
            {
              name: obj.one_data.name,
              type: 'bar',
              barWidth: 10,
              smooth: true,
              itemStyle: { normal: { areaStyle: { type: 'default' } } },
              data: obj.one_data.data,
              itemStyle: {
                normal: {
                  color: '#4EDAB5'
                }
              },
              markPoint: {
                data: [
                  { type: 'max', name: '最大值' },
                  { type: 'min', name: '最小值' }
                ]
              },
              markLine: {
                itemStyle: {
                  normal: { lineStyle: { type: 'solid' }, label: { show: true, position: 'middle' } }
                },
                large: true,   
                effect: {
                  show: false,
                  loop: true,
                  period: 0,
                  scaleSize: 2,
                  color: null,
                  shadowColor: null,
                  shadowBlur: null
                },
                data: [
                  {
                    name: '区级平均分',
                    type: 'average',
                    // 单独配置每条线的样式
                    yAxis: 86,
                    lineStyle: {
                      color: 'blue',
                    },
                    label: {
                      formatter: '区级平均分'
                    }
                  },
                  {
                    name: '校级平均分',
                    yAxis: 89,
                    // 单独配置每条线的样式
                    lineStyle: {
                      color: 'red'
                    },
                    label: {
                      formatter:'校级平均分'
                    }
                  },
                  {
                    name: '任教班级平均分',
                    yAxis: 80,
                    // 单独配置每条线的样式
                    lineStyle: {
                      color: 'green'
                    },
                    label: {
                      formatter: '任教班级平均分'
                    }
                  },
                ]
              }
            },
            {
              name: obj.last_data.name,
              type: 'bar',
              smooth: true,
              barWidth: 10,
              itemStyle: { normal: { areaStyle: { type: 'default' } } },
              data: obj.last_data.data,
              itemStyle: {
                normal: {
                  color: '#7AB6FE'
                }
              },
            }
          ]
        })
        myChart.on('click', function (param) {
          console.log(param, 123456789);
          console.log(param.seriesName);
          console.log(param.name);
          _this.barclick();
        })
      },

      init() {
        this.$api.FindTeachbargraph({})
          .then((response) => {
           // console.log(response.result.data, '老师测试页面');
            this.$store.state.barline = response.result.data;
                })
      },
      //点击班级 联动PR值
      barclick() {
        this.$api.FindClickPR({})
          .then((response) => {
           //console.log(response.result.data,77777)
            this.$store.state.accuracyPR = response.result.data;
          })
      }

    },
    watch: {
      // 		监听指令
      barline(a) {
        if (a) {
          this.drawLine(a)
        }
      },
    }
              }
</script>
<style scoped>
  .barline_box {
    width:100%;
    height:450px;
  }
  #myChart {
    padding: 4% 2% 0% 2% !important;
  }
</style>
