<template>
  <div class="radargraph_box">
    <!--<div class="bargraph_title"><p>各项科目表现</p></div>-->
    <div id="myChart_lei" :style="{width: '100%', height: '450px'}"></div>
  </div>
</template>

<script>
    export default {
        name: "radargraph",
        data(){
          return{

          }
    },
    created() {
      this.init();
        },
      computed:{
        //  	命令发布监听
        //mode () {
        //  return this.$store.state.leida.mode
        //},
        leida () {
          return this.$store.state.leida
        },
      },
      methods:{
        drawLine(obj) {
          this.$store.state.leida.mode = false;
          let myChart_lei = this.$echarts.init(document.getElementById('myChart_lei'));
          myChart_lei.setOption({
            title: {
              text: '各项科目表现'
            },
            tooltip: {},
            legend: {
              top: 25,
              itemWidth: 16,
              itemHeight: 16,
              data: obj.titledata,
              textStyle: {
                color: '#000'
              }
            },
            radar: {
              radius: '60%',
              splitNumber: 8,
              axisLine: {
                lineStyle: {
                  color: '#000',
                  opacity: .2
                }
              },
              splitLine: {
                lineStyle: {
                  color: '#fff',
                  opacity: .2
                }
              },
              splitArea: {
                areaStyle: {
                  color: 'rgba(127,95,132,.2)',
                  opacity: 1,
                  shadowBlur: 45,
                  shadowColor: 'rgba(0,0,0,.5)',
                  shadowOffsetX: 0,
                  shadowOffsetY: 15,
                }
              },
              indicator: obj.indicator
            },
            series: [{
              name: obj.dataname,
              type: 'radar',
              symbolSize: 0,
              areaStyle: {
                normal: {
                  shadowBlur: 13,
                  shadowColor: 'rgba(0,0,0,.2)',
                  shadowOffsetX: 0,
                  shadowOffsetY: 10,
                  opacity: 1
                }
              },
              data: [{
                value: obj.data1,
                name: obj.title_one,
                itemStyle: {
                  color: 'rgba(239,75,76,.5)',
                  lineStyle: {
                    color: 'rgba(239,75,76,.5)',
                  },
                      }
              }, {
                value:obj.data2,
                name: obj.title_two,
                itemStyle: {
                  color: 'rgba(177,234,219,.8)',
                  lineStyle: {
                    color: 'rgba(177,234,219,.8)',
                  },
                }
              }]
            }],
            color: ['#ef4b4c', '#b1eadb'],
            backgroundColor: {
              type: 'radial',
              x: 0.4,
              y: 0.4,
              r: 0.35,
              colorStops: [{
                offset: 0,
                color: '#D4CED5' // 0% 处的颜色
              }, {
                offset: .4,
                color: '#f5f5f5' // 100% 处的颜色
              }, {
                offset: 1,
                color: '#fff' // 100% 处的颜色
              }],
            }
          });
        },
        init() {
          this.$api.FindRadargraph({})
              .then((response) => {
                //console.log(response.result,456);
               this.$store.state.leida=response.result.data
                //this.drawLine(response.result.data)
                //console.log(this.$store.state.zhuzhuang,33333344)
              })
          }
      },
      watch:{
// 		监听指令
        leida(a){
          if(a){
            this.drawLine(a)
          }
        },
      }
    }
</script>
