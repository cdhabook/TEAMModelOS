<template>
  <Row class="linechart_box">
    <Col span="24" class="select_box">
      <div class="type_box">
        <Select v-model="model1" style="width:180px;height:40px;" @on-change="ExamType">
          <Option v-for="item in linechart" :value="item.value" :key="item.value" >{{ item.label }}</Option>
        </Select>
      </div>
      <div class="artsscience" v-if="this.now_url =='/saindex'">
        <li class="wenke" @click="arts_btn"   v-if="wenli_show">显示文科</li>
        <li class="like" @click="science_btn"  v-if="wenli_show">显示理科</li>
      </div>
    </Col>
    <Col span="24">
      <div id="myChart_line" :style="{width: '100%', height: '300px'}"></div>
    </Col>
  </Row>
</template>
<script>
  export default {
    name: "linechart",
    data() {
      return {
        cityList: [],
        model1: 'liankao',
        now_url: this.$route.path,
      }
    },
    created() {
      this.init();
      this.type_init();
          },
    computed: {
      //  	命令发布监听
      //mode() {
      //  return this.$store.state.linechart.mode
      //},
      linechart_standard() {
        return this.$store.state.linechart
          },
      linechart() {
        return this.$store.state.examtype
      },
      wenli_show() {
        return this.$store.state.wenli_show
      }
    },
    methods: {
      drawLine(obj) {
        //this.$store.state.linechart.mode = false;
        let myChart = this.$echarts.init(document.getElementById('myChart_line'));
        myChart.setOption({
          title: {
            text: '各校历次总分统计图',
          },
          legend: {
            data: obj.obj_name,
            top: '10%',
          },
          tooltip: {
            trigger: 'axis',
            //backgroundColor: 'rgba(0,0,0,0.7)',//背景颜色（此时为默认色）
            borderRadius: 8,//边框圆角
            padding: 10,    // [5, 10, 15, 20] 内边距
            //echarts点击事件
            // formatter: function (params,ticket,callback) {
            //   console.log(params)
            // },
          },
          grid: {
            left: '2%',
            right: '3%',
            bottom: '3%',
            containLabel: true
          },
          toolbox: {
            show: true, //是否显示
            orient: 'horizontal', //水平显示(vertical为垂直显示)
            right: '3%', //距离右边多远
            feature: {
              magicType: {//动态类型切换
                type: ['bar', 'line']
              },
              saveAsImage: {
                show: true
              }
            },
          },
          xAxis: {
            type: 'category',
            boundaryGap: false,
            data: obj.obj_data,
            min: 'dataMin',
            max: 'dataMax',
            splitLine: {
              show: true
            }
          },
          yAxis: {
            type: 'value',
            min: 'dataMin',
            max: 'dataMax',
            splitLine: {
              show: true
            }
          },
          series: [
            {
              name: obj.obj_series[0].name,
              type: 'line',
              data: obj.obj_series[0].data,
              itemStyle: {
                normal: {
                  color: '#FF3030',
                  lineStyle: {
                    color: '#FF3030',
                    width: 2,
                  }
                }
              },
              areaStyle: {
                color: {
                  type: 'linear',
                  x: 0,
                  y: 0,
                  x2: 0,
                  y2: 1,
                  colorStops: [{
                    offset: 0, color:obj.start_color // 0% 处的颜色
                  }, {
                    offset: 1, color: obj.end_color // 100% 处的颜色
                  }],
                }
              },
            },
            {
              name: obj.obj_series[1].name,
              type: 'line',
              data: obj.obj_series[1].data,
              itemStyle: {
                normal: {
                  color: "#00F5FF",
                  lineStyle: {
                    color: '#00F5FF',
                    width: 2,
                  }
                }
              },
              areaStyle: {
                color: {
                  type: 'linear',
                  x: 0,
                  y: 0,
                  x2: 0,
                  y2: 1,
                  colorStops: [{
                    offset: 0, color: 'rgba(245,245,245,1)' // 0% 处的颜色
                  }, {
                    offset: 1, color: 'rgba(245,245,245,.5)' // 100% 处的颜色
                  }],
                }
              },
            },
            {
              name: obj.obj_series[2].name,
              type: 'line',
              data: obj.obj_series[2].data,
              itemStyle: {
                normal: {
                  color: "#4169E1",
                  lineStyle: {
                    color: '#4169E1',
                    width: 2,
                  }
                }
              },
              areaStyle: {
                color: {
                  type: 'linear',
                  x: 0,
                  y: 0,
                  x2: 0,
                  y2: 1,
                  colorStops: [{
                    offset: 0, color: 'rgba(245,245,245,1)' // 0% 处的颜色
                  }, {
                    offset: 1, color: 'rgba(245,245,245,.5)' // 100% 处的颜色
                  }],
                }
              },
            },
            {
              name: obj.obj_series[3].name,
              type: 'line',
              data: obj.obj_series[3].data,
              itemStyle: {
                normal: {
                  color: "#912CEE",
                  lineStyle: {
                    color: '#912CEE',
                    width: 2,
                  }
                }
              },
              areaStyle: {
                color: {
                  type: 'linear',
                  x: 0,
                  y: 0,
                  x2: 0,
                  y2: 1,
                  colorStops: [{
                    offset: 0, color: 'rgba(245,245,245,1)' // 0% 处的颜色
                  },  {
                    offset: 1, color: 'rgba(245,245,245,.5)' // 100% 处的颜色
                  }],
                }
              },
            },
            {
              name: obj.obj_series[4].name,
              type: 'line',
              data: obj.obj_series[4].data,
              itemStyle: {
                normal: {
                  color: "#FF8247",
                  lineStyle: {
                    color: '#FF8247',
                    width: 2,
                  }
                }
              },
              areaStyle: {
                color: {
                  type: 'linear',
                  x: 0,
                  y: 0,
                  x2: 0,
                  y2: 1,
                  colorStops: [{
                    offset: 0, color: 'rgba(245,245,245,1)' // 0% 处的颜色
                  },  {
                    offset: 1, color: 'rgba(245,245,245,.5)' // 100% 处的颜色
                  }],
                }
              },
            },
            {
              name: obj.obj_series[5].name,
              type: 'line',
              data: obj.obj_series[5].data,
              itemStyle: {
                normal: {
                  color: "#6AD4B9",
                  lineStyle: {
                    color: '#6AD4B9',
                    width: 2,
                  }
                }
              },
              areaStyle: {
                color: {
                  type: 'linear',
                  x: 0,
                  y: 0,
                  x2: 0,
                  y2: 1,
                  colorStops: [{
                    offset: 0, color: 'rgba(245,245,245,1)' // 0% 处的颜色
                  },  {
                    offset: 1, color: 'rgba(245,245,245,.5)' // 100% 处的颜色
                  }],

                }
              },
            }
          ]
        })
        myChart.on("click", function (e) {
          console.log(e);
          console.log(e.seriesName)
          console.log(e.name)
        })
      },
      init() {
        if (this.now_url == '/saindex') {
          this.$api.FindLinechart({})
            .then((response) => {
              //console.log(response.result.data,11111)
              this.$store.state.linechart = response.result.data;
            })
        } else if (this.now_url =='/teach') {
          this.$api.FindTeachHistory({})
            .then((response) => {
              //console.log(response.result.data, 8877822228888)
              this.$store.state.linechart = response.result.data;
            })
        }
       
      },
      type_init() {
        this.$api.FindExamtype({})
          .then((response) => {
            //console.log(response.result.data, 111)
            //this.cityList = response.result.data;
            this.$store.state.examtype = response.result.data;
          })
      },
      //考试类型
      ExamType(value) {
        this.$api.FindSelectExamType({})
          .then((response) => {
            //console.log(response.result.data, 8889)
            this.$store.state.selectgrade = response.result.data.selectgrade
            //获取新的学年期
            this.$store.state.selectterm = response.result.data.term
            //获取新的考试次数情况
            this.$store.state.selectexam = response.result.data.exam[0].data;
            //获取新的基础信息
            this.$store.state.basicsdata = response.result.data.base;
            //获取新的各校成绩排名
            this.$store.state.zhuxhuang = response.result.data.barecharts;
            //获取新的各项科目表现
            this.$store.state.leida = response.result.data.leida;
            //获取新的科目表现对比
            this.$store.state.pie = response.result.data.pie
            //获取新的考试类型
            this.$store.state.examtype = response.result.data.examtype;
            //获取新的各校历次总分统计图
            this.$store.state.linechart = response.result.data.linechart;
          })
      },
      //显示文科 
      arts_btn() {
        this.$api.FindSelectArts({})
          .then((response) => {
           // console.log(response.result.data, 5555555);
            this.$store.state.selectgrade = response.result.data.selectgrade
            //获取新的学年期
            this.$store.state.selectterm = response.result.data.term
            //获取新的考试次数情况
            this.$store.state.selectexam = response.result.data.exam[0].data;
            //获取新的基础信息
            this.$store.state.basicsdata = response.result.data.base;
            //获取新的各校成绩排名
            this.$store.state.zhuxhuang = response.result.data.barecharts;
            //获取新的各项科目表现
            this.$store.state.leida = response.result.data.leida;
            //获取新的科目表现对比
            this.$store.state.pie = response.result.data.pie
            //获取新的考试类型
            this.$store.state.examtype = response.result.data.examtype;
            //获取新的各校历次总分统计图
            this.$store.state.linechart = response.result.data.linechart;
                })
      },
      //显示理科
      science_btn() {
        this.$api.FindSelectScience({})
          .then((response) => {
            //console.log(response.result.data,77899)
            this.$store.state.selectgrade = response.result.data.selectgrade
            //获取新的学年期
            this.$store.state.selectterm = response.result.data.term
            //获取新的考试次数情况
            this.$store.state.selectexam = response.result.data.exam[0].data;
            //获取新的基础信息
            this.$store.state.basicsdata = response.result.data.base;
            //获取新的各校成绩排名
            this.$store.state.zhuxhuang = response.result.data.barecharts;
            //获取新的各项科目表现
            this.$store.state.leida = response.result.data.leida;
            //获取新的科目表现对比
            this.$store.state.pie = response.result.data.pie
            //获取新的考试类型
            this.$store.state.examtype = response.result.data.examtype;
            //获取新的各校历次总分统计图
            this.$store.state.linechart = response.result.data.linechart;
          })

      }
    },
    watch: {
// 		监听指令
      linechart_standard(a) {
        if (a){
          this.drawLine(a);
        }
      },
    }
  }
</script>
