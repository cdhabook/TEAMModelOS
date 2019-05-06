<template>
    <div :id="id" style="height: 100%;width:100%;"></div>
</template>

<script>
export default {
    data () {
        return {}
    },
    props:{
        id:{
            type: String
        }
    },
    mounted(){
        this.drawLine();
    },
    methods:{
        drawLine(){
            let _this = this
            // 基于准备好的dom，初始化echarts实例
            let myChart = this.$echarts.init(document.getElementById(this.id));
            let arrlabel = ["0-50", "51-60", "61-70", "71-80", "81-90", "90+"];
            let arrTotal1 = [3042,884,1060,1430,1246,70];
            let arrTotal2 = [4175,909,1125,933,566,24];
            let arrTotal3 = [2042,784,1260,1130,746,70];
            myChart.setOption({
                backgroundColor: 'rgba(17, 17, 17, 0.14)',
                tooltip: {
                    trigger: 'axis',
                    borderRadius: 0,
                },
                grid: {
                    left: '0',
                    right: '0',
                    bottom: '5%',
                    top: '10',
                    containLabel: true,
                },
                xAxis: [{
                    type: 'category',
                    position: "bottom",
                    axisLabel: {
                        margin: 10,
                        textStyle: {
                            fontSize: 11,
                            color: '#fafafa'
                        },
                        interval:0,
                        // formatter:function(val){
                        //     let firstOne = arrlabel[0];
                        //     let labellength = arrlabel.length
                        //     let lastOne = arrlabel[labellength-1];
                        //     if(firstOne != val && lastOne != val){
                        //         return val
                        //     }
                        // }
                    },
                    boundaryGap: true,
                    splitLine: {
                        lineStyle: {
                            color: 'rgba(185, 193, 173, 0.63)',
                        },
                        show: true
                    },
                    axisLine: {
                        lineStyle: {
                            color: 'rgba(185, 193, 173, 0.63)',
                            width: 1,
                        }
                    },
                    axisTick: {
                        show: false,
                    },
                    data: arrlabel
                }],
                yAxis: [{
                    type: 'value',
                    // name: '单 位（%）',
                    axisTick: {
                        show: false
                    },
                    axisLine: {
                        show: true,
                        lineStyle: {
                            color: 'rgba(185, 193, 173, 0.63)',
                        }
                    },
                    axisLabel: {
                        margin: 10,
                        textStyle: {
                            fontSize: 11,
                            color:'#fafafa'
                        }
                    },
                    splitLine: {
                        show: true,
                        lineStyle: {
                            color: 'rgba(185, 193, 173, 0.63)',
                        }
                    }
                }],
                series: [{
                    type: 'line',
                    zlevel: 10,
                    lineStyle: {
                        normal: {
                            width: 2
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: '#48DBFC',
                        },
                        emphasis: {
                            // color: 'rgb(0,196,132)',
                            borderColor: 'rgba(0,196,132,0.2)',
                            extraCssText: 'box-shadow: 8px 8px 8px rgba(0, 0, 0, 1);',
                            // borderWidth: 10
                        }
                    },
                    data: arrTotal3
                },
                {
                  name: '科技互动',
                  type: 'bar',
                  barWidth: 10,
                  data: arrTotal1,
                  itemStyle: {
                    normal: {  //渐变色
                      color: new this.$echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: "rgba(28, 208, 161, 1)" // 0% 处的颜色
                      },{
                        offset: 1,
                        color: "rgba(28, 208, 161, 0.5)" // 100% 处的颜色
                      }], false)
                    }
                  }
                },
                {
                  name: '教法应用',
                  type: 'bar',
                  barWidth: 10,
                  data: arrTotal2,
                  itemStyle: {
                    normal: {  //渐变色
                      color: new this.$echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: "rgba(28, 208, 161, 1)" // 0% 处的颜色
                      },{
                        offset: 1,
                        color: "rgba(28, 208, 161, 0.5)" // 100% 处的颜色
                      }], false)
                    }
                  }
                },
                ]
            });
        }
    }
}

</script>

<style>
</style>
