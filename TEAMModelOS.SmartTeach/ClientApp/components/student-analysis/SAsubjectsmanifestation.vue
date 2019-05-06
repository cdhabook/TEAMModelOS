<template>
  <div class="subjects_box">
    <div class="subjects_title"><p>科目表现对比</p></div>
      <li class="subjects_listbox">
        <div class="top">
          <p class="num">{{list_data[0].score}}</p>
          <p class="datatitle">{{list_data[0].title}}</p>
        </div>
        <div class="center">
          <div id="myChart_one" :style="{width: '100%', height: '150px'}"></div>
        </div>
        <div class="bottom">
          <p class="num">{{list_data[0].belowscore}}<Icon :type="list_data[0].icon_type" :class="list_data[0].icon_color" /></p>
          <p class="datatitle">{{list_data[0].below_title}}</p>
        </div>
      </li>
    <li class="subjects_listbox">
      <div class="top">
        <p class="num">{{list_data[1].score}}</p>
        <p class="datatitle">{{list_data[1].title}}</p>
      </div>
      <div class="center">
        <div id="myChart_two" :style="{width: '100%', height: '150px'}"></div>
      </div>
      <div class="bottom">
        <p class="num">{{list_data[1].belowscore}}<Icon :type="list_data[1].icon_type"  :class="list_data[1].icon_color" /></p>
        <p class="datatitle">{{list_data[1].below_title}}</p>
      </div>
    </li>
    <li class="subjects_listbox">
      <div class="top">
        <p class="num">{{list_data[2].score}}</p>
        <p class="datatitle">{{list_data[2].title}}</p>
      </div>
      <div class="center">
        <div id="myChart_three" :style="{width: '100%', height: '150px'}"></div>
      </div>
      <div class="bottom">
        <p class="num">{{list_data[2].belowscore}}<Icon :type="list_data[2].icon_type" :class="list_data[2].icon_color" /></p>
        <p class="datatitle">{{list_data[2].below_title}}</p>
      </div>
    </li>
    <li class="subjects_listbox">
      <div class="top">
        <p class="num">{{list_data[3].score}}</p>
        <p class="datatitle">{{list_data[3].title}}</p>
      </div>
      <div class="center">
        <div id="myChart_four" :style="{width: '100%', height: '150px'}"></div>
      </div>
      <div class="bottom">
        <p class="num">{{list_data[3].belowscore}}<Icon :type="list_data[3].icon_type" :class="list_data[3].icon_color" /></p>
        <p class="datatitle">{{list_data[3].below_title}}</p>
      </div>
    </li>
    <li class="subjects_listbox">
      <div class="top">
        <p class="num">{{list_data[4].score}}</p>
        <p class="datatitle">{{list_data[4].title}}</p>
      </div>
      <div class="center">
        <div id="myChart_five" :style="{width: '100%', height: '150px'}"></div>
      </div>
      <div class="bottom">
        <p class="num">{{list_data[4].belowscore}}<Icon :type="list_data[4].icon_type" :class="list_data[4].icon_color" /></p>
        <p class="datatitle">{{list_data[4].below_title}}</p>
      </div>
    </li>
    <li class="subjects_listbox">
      <div class="top">
        <p class="num">{{list_data[5].score}}</p>
        <p class="datatitle">{{list_data[5].title}}</p>
      </div>
      <div class="center">
        <div id="myChart_six" :style="{width: '100%', height: '150px'}"></div>
      </div>
      <div class="bottom">
        <p class="num">{{list_data[5].belowscore}}<Icon :type="list_data[5].icon_type" :class="list_data[5].icon_color" /></p>
        <p class="datatitle">{{list_data[5].below_title}}</p>
      </div>
    </li>
  </div>
</template>

<script>
    export default {
        name: "SubjectsManifestation",
        data(){
          return{
            list_data: [],
          }
    },
    created() {
      this.init();

    },
    mounted() {
      this.init();
    },
      computed:{
        //  	命令发布监听
        inquiredata() {
          return this.$store.state.pie
        },
    },

      methods:{
          //图形一

        One(obj) {
          //this.$store.state.pie.mode = false;
          let myChart = this.$echarts.init(document.getElementById('myChart_one'));
          myChart.setOption({
            grid: {
              left: 0,
              top: 0,
              right: 0,
              bottom: 0
            },
            color: obj.color,
            series: [{
              type: 'pie',
              radius: ['80%', '97%'],
              hoverAnimation: false,
              label: {
                normal: {
                  position: 'center'
                }
              },
              data: [{
                value: obj.occupy,
                name: '占有率',
                label: {
                  normal: {
                    rich: {
                      a: {
                        lineHeight: 10
                      }
                    },
                    formatter: '{d}%',
                    textStyle: {
                      fontSize: 18,
                      fontWeight: 'bold',
                      color: '#555',
                      height: 25,
                      top: 1,
                    }
                  }
                }
              }, {
                  value: obj.surplus,
                name: '不足',
                label: {
                  normal: {
                    rich: {
                      a: {
                        lineHeight: 5
                      }
                    },
                    formatter: '\n' + obj.subject,
                    textStyle: {
                      color: '#555',
                      fontSize: 15,
                      height: 15,
                    }
                  }
                },
                tooltip: {
                  show: false
                },
                itemStyle: {
                  normal: {
                    color: '#ddd'
                  },
                  emphasis: {
                    color: '#ddd'
                  }
                },
                hoverAnimation: false
              }]
            }]
          })
        },
          Two(obj){
          //this.$store.state.pie.mode = false;
          let myChart = this.$echarts.init(document.getElementById('myChart_two'));
          myChart.setOption({
            grid:{
              left:0,
              top:0,
              right:0,
              bottom:0
            },
            color: obj.color,
            series: [{
              type: 'pie',
              radius: ['80%', '97%'],
              hoverAnimation:false,
              label: {
                normal: {
                  position: 'center'
                }
              },
              data: [{
                value: obj.occupy,
                name: '占有率',
                label: {
                  normal: {
                    rich:{
                      a:{
                        lineHeight: 10
                      }
                    },
                    formatter: '{d}%',
                    textStyle: {
                      fontSize: 18,
                      fontWeight:'bold',
                      color:'#555',
                      height:25,
                      top:1,
                    }
                  }
                }
              }, {
                  value: obj.surplus,
                name: '不足',
                label: {
                  normal: {
                    rich:{
                      a:{
                        lineHeight: 5
                      }
                    },
                    formatter: '\n' + obj.subject,
                    textStyle: {
                      color: '#555',
                      fontSize: 15,
                      height:15,
                    }
                  }
                },
                tooltip: {
                  show: false
                },
                itemStyle: {
                  normal: {
                    color: '#ddd'
                  },
                  emphasis: {
                    color: '#ddd'
                  }
                },
                hoverAnimation: false
              }]
            }]
          })
        },
          Three(obj){
          //this.$store.state.pie.mode = false;
          let myChart = this.$echarts.init(document.getElementById('myChart_three'));
          myChart.setOption({
            grid:{
              left:0,
              top:0,
              right:0,
              bottom:0
            },
            color: obj.color,
            series: [{
              type: 'pie',
              radius: ['80%', '97%'],
              hoverAnimation:false,
              label: {
                normal: {
                  position: 'center'
                }
              },
              data: [{
                value: obj.occupy,
                name: '占有率',
                label: {
                  normal: {
                    rich:{
                      a:{
                        lineHeight: 10
                      }
                    },
                    formatter: '{d}%',
                    textStyle: {
                      fontSize: 18,
                      fontWeight:'bold',
                      color:'#555',
                      height:25,
                      top:1,
                    }
                  }
                }
              }, {
                  value: obj.surplus,
                name: '不足',
                label: {
                  normal: {
                    rich:{
                      a:{
                        lineHeight:5
                      }
                    },
                    formatter: '\n' + obj.subject,
                    textStyle: {
                      color: '#555',
                      fontSize: 15,
                      height:15,
                    }
                  }
                },
                tooltip: {
                  show: false
                },
                itemStyle: {
                  normal: {
                    color: '#ddd'
                  },
                  emphasis: {
                    color: '#ddd'
                  }
                },
                hoverAnimation: false
              }]
            }]
          })
        },
          Four(obj){
          //this.$store.state.pie.mode = false;
          let myChart = this.$echarts.init(document.getElementById('myChart_four'));
          myChart.setOption({
            grid:{
              left:0,
              top:0,
              right:0,
              bottom:0
            },
            color: obj.color,
            series: [{
              type: 'pie',
              radius: ['80%', '97%'],
              hoverAnimation:false,
              label: {
                normal: {
                  position: 'center'
                }
              },
              data: [{
                value: obj.occupy,
                name: '占有率',
                label: {
                  normal: {
                    rich:{
                      a:{
                        lineHeight: 10
                      }
                    },
                    formatter: '{d}%',
                    textStyle: {
                      fontSize: 18,
                      fontWeight:'bold',
                      color:'#555',
                      height:25,
                      top:1,
                    }
                  }
                }
              }, {
                  value: obj.surplus,
                name: '不足',
                label: {
                  normal: {
                    rich:{
                      a:{
                        lineHeight:5
                      }
                    },
                    formatter: '\n' + obj.subject,
                    textStyle: {
                      color: '#555',
                      fontSize: 15,
                      height:15,
                    }
                  }
                },
                tooltip: {
                  show: false
                },
                itemStyle: {
                  normal: {
                    color: '#ddd'
                  },
                  emphasis: {
                    color: '#ddd'
                  }
                },
                hoverAnimation: false
              }]
            }]
          })
        },
          Five(obj){
          //this.$store.state.pie.mode = false;
          let myChart = this.$echarts.init(document.getElementById('myChart_five'));
          myChart.setOption({
            grid:{
              left:0,
              top:0,
              right:0,
              bottom:0
            },
            color:obj.color,
            series: [{
              type: 'pie',
              radius: ['80%', '97%'],
              hoverAnimation:false,
              label: {
                normal: {
                  position: 'center'
                }
              },
              data: [{
                value: obj.occupy,
                name: '占有率',
                label: {
                  normal: {
                    rich:{
                      a:{
                        lineHeight: 10
                      }
                    },
                    formatter: '{d}%',
                    textStyle: {
                      fontSize: 18,
                      fontWeight:'bold',
                      color:'#555',
                      height:25,
                      top:1,
                    }
                  }
                }
              }, {
                  value: obj.surplus,
                name: '不足',
                label: {
                  normal: {
                    rich:{
                      a:{
                        lineHeight: 5
                      }
                    },
                    formatter: '\n'+obj.subject ,
                    textStyle: {
                      color: '#555',
                      fontSize: 15,
                      height:15
                    }
                  }
                },
                tooltip: {
                  show: false
                },
                itemStyle: {
                  normal: {
                    color: '#ddd'
                  },
                  emphasis: {
                    color: '#ddd'
                  }
                },
                hoverAnimation: false
              }]
            }]
          })
        },
          Six(obj){
          //this.$store.state.pie.mode = false;
          let myChart = this.$echarts.init(document.getElementById('myChart_six'));
          myChart.setOption({
            grid:{
              left:0,
              top:0,
              right:0,
              bottom:0
            },
            color: obj.color,
            series: [{
              type: 'pie',
              radius: ['80%', '97%'],
              hoverAnimation:false,
              label: {
                normal: {
                  position: 'center'
                }
              },
              data: [{
                value: obj.occupy,
                name: '占有率',
                label: {
                  normal: {
                    rich:{
                      a:{
                        lineHeight: 10
                      }
                    },
                    formatter: '{d}%',
                    textStyle: {
                      fontSize: 18,
                      fontWeight:'bold',
                      color:'#555',
                      height:25,
                      top:1,
                    }
                  }
                }
              }, {
                  value: obj.surplus,
                name: '不足',
                label: {
                  normal: {
                    rich:{
                      a:{
                        lineHeight: 5
                      }
                    },
                    formatter: '\n' + obj.subject,
                    textStyle: {
                      color: '#555',
                      fontSize: 15,
                      height:15,
                    }
                  }
                },
                tooltip: {
                  show: false
                },
                itemStyle: {
                  normal: {
                    color: '#ddd'
                  },
                  emphasis: {
                    color: '#ddd'
                  }
                },
                hoverAnimation: false
              }]
            }]
          })
        },
        init() {
          let _this = this;
          this.$api.FindSubjectsManifestation({})
            .then((response) => {
              //console.log(response.result.data, 8888888);
              //this.One(response.result.data);
              _this.$store.state.pie = response.result.data;
              //_this.Two(response.result.data[1]);
              //_this.Three(response.result.data[2]);
              //_this.Four(response.result.data[3]);
              //_this.Five(response.result.data[4]);
              //_this.Six(response.result.data[5]);

            })
        }
          },
      watch:{
// 		监听指令
        inquiredata(value){
          if(value){
            this.list_data = value;
            let dataone= value[0];
            let datatwo = value[1];
            let datathree = value[2];
            let datafour = value[3];
            let datafive = value[4];
            let datasix = value[5];
            this.One(dataone);
            this.Two(datatwo);
            this.Three(datathree);
            this.Four(datafour);
            this.Five(datafive);
            this.Six(datasix);
          }
        },
      }
    }
</script>

