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
            let rich = {
                b: {
                    color: "#94998a",
                    fontSize: 10,
                    align: 'left',
                    padding: [3, 0],
                },
                c: {
                    color: "#FAFAFA",
                    fontSize: 10,
                    fontWeight: 100,
                },
                d: {
                    color: "#FAFAFA",
                    fontSize: 15,
                    fontWeight: 100,
                }
            }
            let color = ['#FF6B6A', '#48DBFC', '#1CD0A1', '#FDC958'];
            // 基于准备好的dom，初始化echarts实例
            let myChart = this.$echarts.init(document.getElementById(this.id));
            myChart.setOption({
                tooltip: {
                    formatter: function(p){

                    }
                },
    series: [
        {
            // name:'访问来源',
            type:'pie',
            selectedMode: 'single',
            radius: [0, '46%'],
            center: ['50%', '50%'],
            itemStyle:{
                color: function(p){
                    return color[p.data.type]
                }
            },
            label: {
                normal: {
                    position: 'inner',
                    formatter: function(p){
                        let percent = Math.round(p.percent)
                        return '{title|' + p.name + '}\n{num|' + percent +'%}';
                    },
                    rich: { 
                        title:{
                            color: "#fafafa",
                            fontSize: 10,
                        },
                        num:{
                            color: "#fafafa",
                            fontSize: 10,
                        }
                    }
                }
            },
            labelLine: {
                normal: {
                    show: false
                }
            },
            data:[
                {value:300, name:'语文类别', type: 0},
                {value:500, name:'数理类别', type: 1},
                {value:270, name:'文史类别', type: 2},
                {value:250, name:'艺能类别', type: 3}
            ]
        },
        {
            color: ['#FF6B6A', '#48DBFC', '#1CD0A1', '#FDC958'],
            name:'访问来源',
            type:'pie',
            radius: ['55%', '69%'],
            center: ['50%', '50%'],
            itemStyle : {
                color:function(p){
                    return color[p.data.type]
    	        },
            },
            label: {
                normal: {
                    formatter: function(p){
                        let percent = Math.round(p.percent).toFixed(1)
                        return '{b|' + p.name + '}\n{d|' + percent +'% }{c|('+ p.value +')}';
                    },
                    rich: rich
                },
                emphasis: {
                    show: true,
                    textStyle: {
                    }
                }
            },
            data:[
                {value:200, name:'国语文', type: 0},
                {value:100, name:'英语文', type: 0},
                {value:100, name:'数学', type: 1},
                {value:140, name:'生物', type: 1},
                {value:70, name:'化学', type: 1},
                {value:190, name:'物理', type: 1},
                {value:190, name:'历史', type: 2},
                {value:80, name:'地理', type: 2},
                {value:125, name:'体育', type: 3},
                {value:125, name:'舞蹈', type: 3}
            ]
        }
    ]
            });
        }
    }
}

</script>

<style>
</style>
