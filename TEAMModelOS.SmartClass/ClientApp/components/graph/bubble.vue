<template>
    <div :id="id" style="height: 100%;width:100%;"></div>
</template>

<script>
export default {
    data () {
        return {
            mychat: '',
            total: 0,
            heightlightIndex: 0,
        }
    },
    props:{
        id:{
            type: String
        },
        // pieData:{
        //     type: Array,
        //     default: function(){
        //         return [
        //             {value:335, name:'新增檔案'},
        //             {value:310, name:'書面問答'},
        //             {value:234, name:'匯入.pptx'},
        //             {value:135, name:'PowerClick'},
        //             {value:1548, name:'開啟.hte'},
        //             {value:123, name:'其他'},
        //         ]
        //     },
        // },
        title:{
            type: String
        },
        ylabel:{
            type: Array,
            default: function(){
                return ['一年級', '二年級', '三年級','四年級','五年級'];
            }
        },
        bubleData:{
            type: Array,
            default: function(){
                // [x, y, total]
                return [
                [0,0,10], [1,0, 30], [4,0, 20], [5,0, 10],
                [1,1,30], [2,1, 30],
                [3,2,30], [4,2, 10],
                [2,3,30]];
            }
        }
    },
    mounted(){
        this.drawLine();
    },
    created(){
    },
    methods:{
        drawLine(){
            let _this = this
            var bubblecolor = ['#FF6B6A', '#FF9FF4', '#48DBFC', '#1CD0A1', '#FDC958', '#FFAD76'];
            var hours = ['0-50', '51-60', '61-70', '71-80', '81-80', '90~'];
            // data = data.map(function (item) {
            //     return [item[1], item[0], item[2]];
            // });
            // console.log(data)
            // 基于准备好的dom，初始化echarts实例
            this.myChart = this.$echarts.init(document.getElementById(this.id));
            this.myChart.setOption({  
              title:{
                show: _this.title ? true : false,
                text: _this.title ? _this.title : '',
                left: 'center',
                textStyle:{
                  color: '#fafafa',
                  fontWeight: 100,
                }
              },
                tooltip: {
                    position: 'top',
                    formatter: function (params) {
                        return _this.ylabel[params.value[1]] + '裡，'+ hours[params.value[0]] + '區間的一共有'+params.value[2] + '人';
                    }
                },
                grid: {
                    left: '2%',
                    bottom: 0,
                    right: '16px',
                    containLabel: true,      
                },
                xAxis: {
                    type: 'category',
                    data: hours,
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
                },
                yAxis: {
                    type: 'category',
                    data: _this.ylabel,
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
                },
                series: [{
                    // name: '论文数量',
                    type: 'scatter',
                    symbolSize: function (val) {
                        let tmp = val[2]*0.03;
                        if(tmp == 0){
                            return 0
                        } else if(tmp<1){
                            return 10
                        } else {
                            return tmp;
                        }                        
                    },
                    data: _this.bubleData,
                    animationDelay: function (idx) {
                        return idx * 5;
                    },
                    itemStyle:{
                        normal:{
                            color: function(params) {
                                var num=bubblecolor.length;
                                return bubblecolor[params.dataIndex%num]
                            },
                        }
                    }
                }]
            });
        },
    }
}

</script>

<style>
</style>
