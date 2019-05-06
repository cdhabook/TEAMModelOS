<template>
  <div class="bargraph">
    <!--<div class="bargraph_title"><p>各校成绩排名</p></div>-->
    <div id="myChart" :style="{width: '100%', height: '460px'}"></div>
  </div>
</template>
<script>
    export default {
        name: "bargraph",
        data(){
          return{
            now_url: this.$route.path,
          }
    },
    created() {
      this.init();
            },
        computed:{

        zhuxhuang () {
          return this.$store.state.zhuxhuang
          },
        
       },
      methods:{
        drawLine(obj) {
          let _this = this;
          this.$store.state.zhuxhuang.mode = false;
          let myChart = this.$echarts.init(document.getElementById('myChart'));
          myChart.setOption({
            title: {
              text: obj.titlename,
              left: "left",
              top:"0%",
              y: "10",
              textStyle:{
                fontSize:16
              }
            },
            tooltip : {
              trigger: 'axis',
              axisPointer : {            // 坐标轴指示器，坐标轴触发有效
                type : 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
              },
              formatter: function (value) {
                let arr = '';
                for (var i = 0; i < value.length / 2; i++) {
                  //console.log(value[i].seriesName,666666)
                  var datalist = value[i].seriesName + ' : ' + value[i].data+'<br>';
                  arr += datalist;
                };
                let one_average = obj.one_average.name + ' : ' + obj.one_average.data + '<br/>';
                let two_average = obj.two_average.name + ' : ' + obj.two_average.data;
                arr+=(one_average);
                arr+=(two_average);
                return arr;
                }
            },
            legend: {
              data:obj.titledata,
              top:"5%"
            },
            grid: {
              left: '2%',
              right: '4%',
              bottom: '3.5%',
              containLabel: true,
              x:100,
              y2:100,
            },
            xAxis : [
              {
                type : 'category',
                data : obj.object_name,
                axisLabel: {
                  interval: 0,    //强制文字产生间隔
                  rotate: 45,     //文字逆时针旋转50°
                  textStyle: {    //文字样式
                    color: "#333",
                    fontSize: 12,
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
            yAxis : [
              {
                type : 'value',
              }
            ],
            series : [
              {
                name:obj.one_data.name,
                type:'bar',
                stack: '本校',
                data:obj.one_data.data,
                barWidth : 20,
                itemStyle:{
                  normal:{
                    color:'#ffb5f6'
                  }
                },
              },
              {
                name: obj.two_data.name,
                type:'bar',
                stack: '本校',
                data: obj.two_data.data,
                barWidth : 20,
                itemStyle:{
                  normal:{
                    color:'#fdd57e'
                  }
                },
              },
              {
                name: obj.three_data.name,
                type:'bar',
                stack: '本校',
                data:obj.three_data.data,
                barWidth : 20,
                itemStyle:{
                  normal:{
                    color:'#ff8d8c'
                  }
                },
              },
              {
                name:obj.four_data.name,
                type:'bar',
                stack: '本校',
                data: obj.four_data.data,
                barWidth : 20,
                itemStyle:{
                  normal:{
                    color:'#73e2fd'
                  }
                },
              },
              {
                name: obj.five_data.name,
                type:'bar',
                stack: '本校',
                data:obj.five_data.data,
                barWidth : 20,
                itemStyle:{
                  normal:{
                    color:'#50dab6'
                  }
                },
              },
              {
                name: obj.six_data.name,
                type:'bar',
                stack: '本校',
                data: obj.six_data.data,
                barWidth : 20,
                itemStyle:{
                  normal:{
                    color:'#6489da'
                  }
                },
              },
              {
                name: obj.quyudata.anotherone_data.name,
                type: 'bar',
                stack: '区域',
                data: obj.quyudata.anotherone_data.datalist,
                barWidth : 20,
                itemStyle:{
                  normal:{
                    color:'#c4cbd1'
                  }
                },
              },
              {
                name: obj.quyudata.anothertwo_data.name,
                type: 'bar',
                stack: '区域',
                data: obj.quyudata.anothertwo_data.datalist,
                barWidth: 20,
                itemStyle: {
                  normal: {
                    color: '#c4cbd1'
                  }
                },
              },
              {
                name: obj.quyudata.anotherthree_data.name,
                type: 'bar',
                stack: '区域',
                data: obj.quyudata.anotherthree_data.datalist,
                barWidth: 20,
                itemStyle: {
                  normal: {
                    color: '#c4cbd1'
                  }
                },
              },
              {
                name: obj.quyudata.anotherfour_data.name,
                type: 'bar',
                stack: '区域',
                data: obj.quyudata.anotherfour_data.datalist,
                barWidth: 20,
                itemStyle: {
                  normal: {
                    color: '#c4cbd1'
                  }
                },
              },
              {
                name: obj.quyudata.anotherfive_data.name,
                type: 'bar',
                stack: '区域',
                data: obj.quyudata.anotherfive_data.datalist,
                barWidth: 20,
                itemStyle: {
                  normal: {
                    color: '#c4cbd1'
                  }
                },
              },
              {
                name: obj.quyudata.anothersix_data.name,
                type: 'bar',
                stack: '区域',
                data: obj.quyudata.anothersix_data.datalist,
                barWidth: 20,
                itemStyle: {
                  normal: {
                    color: '#c4cbd1'
                  }
                },
              },
            ]
          }),
            myChart.on('click', function (param) {
            let schoolname = param.name;
            if (schoolname == '成都七中') {
              _this.cdqz();
            } else if (schoolname == '成都四中') {
              _this.cdsz();
            } else if (schoolname == '成都九中') {
              _this.cdjz();
            } else if (schoolname == '成都树德中学') {
              _this.cdsd();
            } else if (schoolname == '师大一中') {
              _this.sdyz();
            } else if (schoolname == '西川中学') {
              _this.xczx();
            } else if (schoolname == '成都市石室中学') {
              _this.sszx();
            }
            })
        },
        //七中
        cdqz() {
          this.$api.Findcdqz({})
            .then((response) => {
              this.$store.state.leida= response.result.data;
            })
        },
        //四中
        cdsz() {
          this.$api.Findcdsz({})
            .then((response) => {
              this.$store.state.leida = response.result.data;
            })
        },
        //九中
        cdjz() {
          this.$api.Findcdjz({})
            .then((response) => {
              this.$store.state.leida = response.result.data;
            })
        },
        //树德
        cdsd() {
          this.$api.Findcdsd({})
            .then((response) => {
              this.$store.state.leida = response.result.data;
            })
        },
        //师大一中
        sdyz() {
          this.$api.Findsdyz({})
            .then((response) => {
              this.$store.state.leida = response.result.data;
            })
        },
        //西川中学
        xczx() {
          this.$api.Findxczx({})
            .then((response) => {
              this.$store.state.leida = response.result.data;
            })
        },
        //石室中学
        sszx() {
          this.$api.Findsszx({})
            .then((response) => {
              this.$store.state.leida = response.result.data;
            })
        },
        init() {
            this.$api.FindBargraph({})
              .then((response) => {
                this.$store.state.zhuxhuang = response.result.data;
              })
        },
      },
      watch:{
// 		监听指令
        zhuxhuang(a){
          if (a) {
            this.drawLine(a)
          }
        },
      }
    }
</script>
