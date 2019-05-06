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
            let arrlabel = ["2017-11","2017-12","2018-01","2018-02","2018-03","2018-04","2018-05","2018-06","2018-07","2018-08","2018-09","2018-10","2018-11","2018-12","2019-01","2019-02","2019-03","2019-04"];
            let arrTotal1 = [29,245,94,0,310,301,481,207,1,27,713,1053,1312,1064,365,441,720,369];
            let arrTotal2 = ["26", "20", "2", "18", "27", "22", "26", "33", "9", "0", "40"];
            myChart.setOption({
                // backgroundColor: 'rgba(17, 17, 17, 0.14)',
                tooltip: {
                    trigger: 'axis',
                    borderRadius: 0,
                },
                grid: {
                    left: '0',
                    right: '0',
                    bottom: '5%',
                    top: '5%',
                    containLabel: true,
                },
                xAxis: [{
                    axisTick: {
                        show: false,
                    },
                    type: 'category',
                    axisLabel: {
                        margin: 10,
                        textStyle: {
                            fontSize: 11,
                            color: '#fafafa'
                        },
                        // interval:0,
                        // formatter:function(val){
                        //     let firstOne = arrlabel[0];
                        //     let labellength = arrlabel.length
                        //     let lastOne = arrlabel[labellength-1];
                        //     if(firstOne != val && lastOne != val){
                        //         return val
                        //     }
                        // }
                    },
                    boundaryGap: false,
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
                    // name: '实名率',
                    type: 'line',
                    // symbol: 'circle',
                    //symbolSize: 5,
                    // showSymbol: false,
                    // markPoint: { // markLine 也是同理
                    //   data: [{
                    //     coord: [0,1,2,3,4], // 其中 5 表示 xAxis.data[5]，即 '33' 这个元素。
                    //     // coord: ['5', 33.4] // 其中 '5' 表示 xAxis.data中的 '5' 这个元素。
                    //     // 注意，使用这种方式时，xAxis.data 不能写成 [number, number, ...]
                    //     // 而只能写成 [string, string, ...]
                    //   }]
                    // },
                    lineStyle: {
                        normal: {
                            width: 2
                        }
                    },
                    areaStyle: {
                        normal: {
                            // 渐变色
                            color: new this.$echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                offset: 0,
                                color: "rgba(28, 208, 161, 1)" // 0% 处的颜色
                            },{
                                offset: 1,
                                color: "rgba(28, 208, 161, 0.5)" // 100% 处的颜色
                            }], false)
                        },
                    },
                    itemStyle: {
                        normal: {
                            areaStyle: {
                                type: 'default'
                            },
                            color: '#1CD0A1',
                        },
                        emphasis: {
                            // color: 'rgb(0,196,132)',
                            borderColor: 'rgba(0,196,132,0.2)',
                            extraCssText: 'box-shadow: 8px 8px 8px rgba(0, 0, 0, 1);',
                            // borderWidth: 10
                        }
                    },
                    data: arrTotal1
                },
                // {
                //     type: 'line',
                //     lineStyle: {
                //         normal: {
                //             width: 2
                //         }
                //     },
                //     areaStyle: {
                //         normal: {
                //             // 渐变色
                //             color: new this.$echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                //                 offset: 0,
                //                 color: "rgba(72, 219, 252, 1)" // 0% 处的颜色
                //             },{
                //                 offset: 1,
                //                 color: "rgba(72, 219, 252, 0.5)" // 100% 处的颜色
                //             }], false)
                //         },
                //     },
                //     itemStyle: {
                //         normal: {
                //             areaStyle: {
                //                 type: 'default'
                //             },
                //             color: '#48DBFC',
                //         },
                //         emphasis: {
                //             // color: 'rgb(0,196,132)',
                //             borderColor: 'rgba(0,196,132,0.2)',
                //             extraCssText: 'box-shadow: 8px 8px 8px rgba(0, 0, 0, 1);',
                //             // borderWidth: 10
                //         }
                //     },
                //     data: arrTotal2
                // }
                ]
            });
        }
    }
}

</script>

<style>
</style>
