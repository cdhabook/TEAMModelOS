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
            let arrlabel = ["3/15", "3/16", "3/17", "3/18", "3/19", "3/20", "3/21", "3/22", "3/23", "3/24", "TODAY"];
            let arrTotal1 = ["22", "17", "0", "26", "22", "24", "18", "30", "15", "10", "30"];
            let arrTotal2 = ["26", "20", "2", "18", "27", "22", "26", "33", "9", "0", "40"];
            let arrTotal3 = ["40", "26", "8", "1", "34", "8", "54", "16", "16", "25", "20"];
            myChart.setOption({
                backgroundColor: 'rgba(17, 17, 17, 0.14)',
                tooltip: {
                    trigger: 'axis',
                    borderRadius: 0,
                    // backgroundColor:'#7AD1A8',
                    // formatter: function(pm) {
                    //     var param = pm[0];
                    //     var pht = '<span style="display:inline-block;margin-right:5px;width:10px;height:10px;background-color:rgba(166,154,228);"></span>';
                    //     console.log(param.marker);
                    //     var prm = "星期" + "日一二三四五六 ".charAt(new Date(param.name).getDay());
                    //     return param.name + ":&nbsp;&nbsp;" + prm + "<br>" +
                    //         pht + param.seriesName + ":&nbsp;&nbsp;" + param.value + "&nbsp;&nbsp;|&nbsp;&nbsp;80.33ttt%";
                    // },
                    //   axisPointer: { // 坐标轴指示器，坐标轴触发有效
                    //         type: 'none' // 默认为直线，可选为：'line' | 'shadow'
                    //     },
                },
                grid: {
                    left: '0',
                    right: '0',
                    bottom: '5%',
                    top: '0',
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
                            color: '#94998a'
                        },
                        interval:0,
                        formatter:function(val){
                            let firstOne = arrlabel[0];
                            let labellength = arrlabel.length
                            let lastOne = arrlabel[labellength-1];
                            if(firstOne != val && lastOne != val){
                                return val
                            }
                        }
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
                    position: 'right',
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
                        inside: true,
                        textStyle: {
                            fontSize: 14,
                            color:'transparent'
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
                {
                    type: 'line',
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
                                color: "rgba(255, 107, 106, 1)" // 0% 处的颜色
                            },{
                                offset: 1,
                                color: "rgba(255, 107, 106, 0.5)" // 100% 处的颜色
                            }], false)
                        },
                    },
                    itemStyle: {
                        normal: {
                            areaStyle: {
                                type: 'default'
                            },
                            color: '#FF6B6A',
                        },
                        emphasis: {
                            // color: 'rgb(0,196,132)',
                            borderColor: 'rgba(0,196,132,0.2)',
                            extraCssText: 'box-shadow: 8px 8px 8px rgba(0, 0, 0, 1);',
                            // borderWidth: 10
                        }
                    },
                    data: arrTotal2
                },
                {
                    type: 'line',
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
                                color: "rgba(72, 219, 252, 1)" // 0% 处的颜色
                            },{
                                offset: 1,
                                color: "rgba(72, 219, 252, 0.5)" // 100% 处的颜色
                            }], false)
                        },
                    },
                    itemStyle: {
                        normal: {
                            areaStyle: {
                                type: 'default'
                            },
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
                }
                ]
            });
        }
    }
}

</script>

<style>
</style>
