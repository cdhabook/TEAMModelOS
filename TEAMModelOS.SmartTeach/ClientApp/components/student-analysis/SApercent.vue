<template>
  <div class="percent_box">
    <div class="title_box"><p>PR（百分等级）</p></div>
    <!--正确率-->
    <div class="percent_list">
      <div class="piedata_box" v-for="(item,index) in accuracydatas">
        <div class="pie_box">
          <div :id="item.idname" :style="{width: '100%', height: '200px'}"></div>
        </div>
        <div class="pie_title">
          <p class="title_name">高分组答对率</p>
          <p class="title_num">{{item.high_accuracy}}%</p>
          <p class="title_name title_two">低分组答对率</p>
          <p class="title_num">{{item.low_accuracy}}%</p>
        </div>
      </div>
    </div>
    <!--RP百分比-->
    <div class="percent_list">
      <div class="piedata_box" v-for="(items,index) in prdata">
        <div class="pie_box">
          <div :id="items.idname" :style="{width: '100%', height: '200px'}"></div>
        </div>
        <div class="pie_marked">
          <Icon :type="items.icon_type"  :class="items.icon_color"  size="48"/>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  export default {
    data() {
      return {
        accuracydatas: [],
        prdata:[],
      }
    },
    created() {
      this.init();
    },
    mounted() {
      this.init();
    },
    computed: {
      accuracyPRdata() {
        return this.$store.state.accuracyPR
      },
    },
    methods: {
      //本班准确率
      //答对率
      accuracy_one(obj) {
        //this.$store.state.pie.mode = false;
        let myChart = this.$echarts.init(document.getElementById('pie_one'));
        myChart.setOption({
          title: {
            text: obj.titlenum + '%',
            subtext: obj.echartstitle,
            x: 'center',
            y: '35%',
            textStyle: {
              fontWeight: 'normal',
              color: '#0580f2',
              fontSize: '22'
            },
            subtextStyle: {
               fontSize: 11,
               fontWeight: 'bolder',
               color: '#333'
             },
          },
          color: ['rgba(176, 212, 251, 1)'],
          legend: {
            show: false,
            itemGap: 12,
            data: ['01', '02']
          },

          series: [{
            name: obj.data_name,
            type: 'pie',
            clockWise: true,
            radius: ['70%', '100%'],
            itemStyle: {
              normal: {
                label: {
                  show: false
                },
                labelLine: {
                  show: false
                }
              }
            },
            hoverAnimation: false,
            data: [{
              value: obj.occupy,
              name: '01',
              itemStyle: {
                normal: {
                  color: { // 完成的圆环的颜色
                    colorStops: [{
                      offset: 0,
                      color: '#00cefc' // 0% 处的颜色
                    }, {
                      offset: 1,
                      color: '#367bec' // 100% 处的颜色
                    }]
                  },
                  label: {
                    show: false
                  },
                  labelLine: {
                    show: false
                  }
                }
              }
            }, {
              name: '02',
                value: obj.residue
            }]
          }]
        })
      },
      accuracy_two(obj) {
        //this.$store.state.pie.mode = false;
        let myChart = this.$echarts.init(document.getElementById('pie_two'));
        myChart.setOption({
          title: {
            text: obj.titlenum +'%',
            subtext: obj.echartstitle,
            x: 'center',
            y: '35%',
            textStyle: {
              fontWeight: 'normal',
              color: '#0580f2',
              fontSize: '22'
            },
            subtextStyle: {
              fontSize: 11,
              fontWeight: 'bolder',
              color: '#333'
            },
          },
          color: ['rgba(176, 212, 251, 1)'],
          legend: {
            show: false,
            itemGap: 12,
            data: ['01', '02']
          },

          series: [{
            name: obj.data_name,
            type: 'pie',
            clockWise: true,
            radius: ['70%', '100%'],
            itemStyle: {
              normal: {
                label: {
                  show: false
                },
                labelLine: {
                  show: false
                }
              }
            },
            hoverAnimation: false,
            data: [{
              value: obj.occupy,
              name: '01',
              itemStyle: {
                normal: {
                  color: { // 完成的圆环的颜色
                    colorStops: [{
                      offset: 0,
                      color: '#00cefc' // 0% 处的颜色
                    }, {
                      offset: 1,
                      color: '#367bec' // 100% 处的颜色
                    }]
                  },
                  label: {
                    show: false
                  },
                  labelLine: {
                    show: false
                  }
                }
              }
            }, {
              name: '02',
              value: obj.residue
            }]
          }]
        })
      },
      accuracy_three(obj) {
        //this.$store.state.pie.mode = false;
        let myChart = this.$echarts.init(document.getElementById('pie_three'));
        myChart.setOption({
          title: {
            text: obj.titlenum + '%',
            subtext: obj.echartstitle,
            x: 'center',
            y: '35%',
            textStyle: {
              fontWeight: 'normal',
              color: '#0580f2',
              fontSize: '22'
            },
            subtextStyle: {
              fontSize: 11,
              fontWeight: 'bolder',
              color: '#333'
            },
          },
          color: ['rgba(176, 212, 251, 1)'],
          legend: {
            show: false,
            itemGap: 12,
            data: ['01', '02']
          },

          series: [{
            name: obj.data_name,
            type: 'pie',
            clockWise: true,
            radius: ['70%', '100%'],
            itemStyle: {
              normal: {
                label: {
                  show: false
                },
                labelLine: {
                  show: false
                }
              }
            },
            hoverAnimation: false,
            data: [{
              value: obj.occupy,
              name: '01',
              itemStyle: {
                normal: {
                  color: { // 完成的圆环的颜色
                    colorStops: [{
                      offset: 0,
                      color: '#00cefc' // 0% 处的颜色
                    }, {
                      offset: 1,
                      color: '#367bec' // 100% 处的颜色
                    }]
                  },
                  label: {
                    show: false
                  },
                  labelLine: {
                    show: false
                  }
                }
              }
            }, {
              name: '02',
              value: obj.residue
            }]
          }]
        })
      },
      //PR值
      pr_one(obj) {
        //this.$store.state.pie.mode = false;
        let myChart = this.$echarts.init(document.getElementById('pr_one'));
        myChart.setOption({
          title: {
            text: obj.titlenum ,
            subtext: obj.echartstitle,
            x: 'center',
            y: '35%',
            textStyle: {
              fontWeight: 'normal',
              color: '#0580f2',
              fontSize: '22'
            },
            subtextStyle: {
              fontSize: 11,
              fontWeight: 'bolder',
              color: '#333'
            },
          },
          color: ['rgba(176, 212, 251, 1)'],
          legend: {
            show: false,
            itemGap: 12,
            data: ['01', '02']
          },

          series: [{
            name: obj.data_name,
            type: 'pie',
            clockWise: true,
            radius: ['70%', '100%'],
            itemStyle: {
              normal: {
                label: {
                  show: false
                },
                labelLine: {
                  show: false
                }
              }
            },
            hoverAnimation: false,
            data: [{
              value: obj.occupy,
              name: '01',
              itemStyle: {
                normal: {
                  color: { // 完成的圆环的颜色
                    colorStops: [{
                      offset: 0,
                      color: '#66CCCC' // 0% 处的颜色
                    }, {
                      offset: 1,
                        color: '#00FFFF' // 100% 处的颜色
                    }]
                  },
                  label: {
                    show: false
                  },
                  labelLine: {
                    show: false
                  }
                }
              }
            }, {
              name: '02',
              value: obj.residue
            }]
          }]
        })
      },
      pr_two(obj) {
        //this.$store.state.pie.mode = false;
        let myChart = this.$echarts.init(document.getElementById('pr_two'));
        myChart.setOption({
          title: {
            text: obj.titlenum ,
            subtext: obj.echartstitle,
            x: 'center',
            y: '35%',
            textStyle: {
              fontWeight: 'normal',
              color: '#0580f2',
              fontSize: '22'
            },
            subtextStyle: {
              fontSize: 11,
              fontWeight: 'bolder',
              color: '#333'
            },
          },
          color: ['rgba(176, 212, 251, 1)'],
          legend: {
            show: false,
            itemGap: 12,
            data: ['01', '02']
          },

          series: [{
            name: obj.data_name,
            type: 'pie',
            clockWise: true,
            radius: ['70%', '100%'],
            itemStyle: {
              normal: {
                label: {
                  show: false
                },
                labelLine: {
                  show: false
                }
              }
            },
            hoverAnimation: false,
            data: [{
              value: obj.occupy,
              name: '01',
              itemStyle: {
                normal: {
                  color: { // 完成的圆环的颜色
                    colorStops: [{
                      offset: 0,
                      color: '#66CCCC' // 0% 处的颜色
                    }, {
                      offset: 1,
                      color: '#00FFFF' // 100% 处的颜色
                    }]
                  },
                  label: {
                    show: false
                  },
                  labelLine: {
                    show: false
                  }
                }
              }
            }, {
              name: '02',
              value: obj.residue
            }]
          }]
        })
      },
      pr_three(obj) {
        //this.$store.state.pie.mode = false;
        let myChart = this.$echarts.init(document.getElementById('pr_three'));
        myChart.setOption({
          title: {
            text: obj.titlenum,
            subtext: obj.echartstitle,
            x: 'center',
            y: '35%',
            textStyle: {
              fontWeight: 'normal',
              color: '#0580f2',
              fontSize: '22'
            },
            subtextStyle: {
              fontSize: 11,
              fontWeight: 'bolder',
              color: '#333'
            },
          },
          color: ['rgba(176, 212, 251, 1)'],
          legend: {
            show: false,
            itemGap: 12,
            data: ['01', '02']
          },

          series: [{
            name: obj.data_name,
            type: 'pie',
            clockWise: true,
            radius: ['70%', '100%'],
            itemStyle: {
              normal: {
                label: {
                  show: false
                },
                labelLine: {
                  show: false
                }
              }
            },
            hoverAnimation: false,
            data: [{
              value: obj.occupy,
              name: '01',
              itemStyle: {
                normal: {
                  color: { // 完成的圆环的颜色
                    colorStops: [{
                      offset: 0,
                      color: '#66CCCC' // 0% 处的颜色
                    }, {
                      offset: 1,
                      color: '#00FFFF' // 100% 处的颜色
                    }]
                  },
                  label: {
                    show: false
                  },
                  labelLine: {
                    show: false
                  }
                }
              }
            }, {
              name: '02',
              value: obj.residue
            }]
          }]
        })
      },
      init() {
        let _this = this;
        this.$api.FindTeachPR({})
          .then((response) => {
            //console.log(response.result.data,88899999)
            _this.$store.state.accuracyPR = response.result.data;
          })
      }
    },
    watch: {
      // 		监听指令
      accuracyPRdata(value) {
        if (value) {
          console.log(value, 6666);
          this.accuracydatas = value.accuracydata;
          this.prdata = value.percentdata;
          let pie_one = value.accuracydata[0];
          let pie_two = value.accuracydata[1];
          let pie_three = value.accuracydata[2];
          let pr_one = value.percentdata[0];
          let pr_two = value.percentdata[1];
          let pr_three = value.percentdata[2];
          this.accuracy_one(pie_one);
          this.accuracy_two(pie_two);
          this.accuracy_three(pie_three);
          this.pr_one(pr_one);
          this.pr_two(pr_two);
          this.pr_three(pr_three);
        }
      },
    }

  }
</script>

