<template>
  <div class="annulus_box">
    <div class="annulus_title"><p>及格率比较</p></div>
    <div id="pie_duo" style="width:99%; height:450px;"></div>
  </div>
</template>
<script>
  export default {
    data() {
      return {
      
      }
    },
    created() {
      this.init();
    },
    computed: {
      annulus() {
        return this.$store.state.annulus
      },
    },
    methods: {
      drawLine(obj) {
        let title = '';
        let num = '';
        console.log(title,456789)
        var dataStyle = {
          normal: {
            label: { show: false },
            labelLine: { show: false },
            // shadowBlur: 40,
            shadowColor: 'rgba(40, 40, 40, 0.5)',
          }
        };
        let myChart_lei = this.$echarts.init(document.getElementById('pie_duo'));
        myChart_lei.setOption({
           title:{
             text:title   ,
             subtext: this.num,
             x:'center',
             y:'35%',
             itemGap: 0,
             textStyle: {
               fontSize: 28,
               fontWeight: 'bolder',
               color: '#000080'
             },
             //subtextStyle: {
             //  fontSize: 19,
             //  fontWeight: 'bolder',
             //  color: '#333'
             //},
          
           },
          backgroundColor: '#fff',
          color: obj.primarycolor,
          tooltip: {
            show: true,
            position: ['33%', '41%'],
            fontSize:'30',
            formatter: function (value) {
              var res = '<div style="text-align:center;width:120px;"><p style="font-size:16px;">' + value.name + '</p></div>' + '<br/>' +
                '<div style="text-align:center;width:120px;"><p style="color:#00FFFF;font-size:20px;">' + value.value + '%' + '</p></div>'
              return res;
            },
          },
          legend: {
            show: false,
            itemGap: 12,
            top: '0%',
            right: '4%',
            data: obj.title
          },
          toolbox: {
            show: false,
            feature: {
              mark: { show: true },
              dataView: { show: true, readOnly: false },
              restore: { show: true },
              saveAsImage: { show: true }
            }
          },
          series: [
            {
              name: obj.areadata.dataname,
              type: 'pie',
              clockWise: true,
              radius: ['75%', '90%'],
              itemStyle: dataStyle,
              hoverAnimation: false,
              data: [
                {
                  value: obj.areadata.occupydata,
                  name: '本区及格率'
                },
                {
                  value: obj.areadata.remaindata,
                  name: '本区未及格率',
                  itemStyle: {
                    normal: {
                      color: '#BFEFFF',
                    }
                  }
                },
              ]
            },
            {
              name: obj.schooldata.dataname,
              type: 'pie',
              clockWise: true,
              hoverAnimation: false,
              radius: ['60%', '75%'],
              itemStyle: dataStyle,
              data: [
                {
                  value: obj.schooldata.occupydata,
                  name: '本校及格率'
                },
                {
                  value: obj.schooldata.remaindata,
                  name: '本校未及格率',
                  itemStyle: {
                    normal: {
                      color: '#B2DFEE',
                    }
                  }
                }
              ]
            },
            {
              name: obj.teachingdata.dataname,
              type: 'pie',
              clockWise: true,
              hoverAnimation: false,
              radius: ['45%', '60%'],
              itemStyle: dataStyle,
              data: [
                {
                  value: obj.teachingdata.occupydata,
                  name: '任教班及格率'
                },
                {
                  value: obj.teachingdata.remaindata,
                  name: '任教班未及格率',
                  itemStyle: {
                    normal: {
                      color: '#F2F2F2',
                    }
                  }
                }
              ]
            },
          ]
        })
      },

      init() {
        this.$api.FindTeachAnnulus({})
          .then((response) => {
            //console.log(response.result.data, 777777)
            this.$store.state.annulus = response.result.data
          })
      },
    },
    watch: {
      // 		监听指令
      annulus(a) {
          this.drawLine(a)
      },
    }
  }
</script>
<style scoped>
  .annulus_box {
    width:100%;
    height:450px;
  }
  .annulus_title {
    width:100%;
    font-size:18px;
    font-weight:bold;
    color:#333;
    padding-left:3%;
    padding-top:1%;
    
  }
</style>
