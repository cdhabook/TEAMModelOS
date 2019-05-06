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
        pieData:{
            type: Array,
            default: function(){
                return [
                    {value:335, name:'新增檔案'},
                    {value:310, name:'書面問答'},
                    {value:234, name:'匯入.pptx'},
                    {value:135, name:'PowerClick'},
                    {value:1548, name:'開啟.hte'},
                    {value:123, name:'其他'},
                ]
            },
        },
        defaultActive: {
            type: Boolean,
            default: function(){
                return false;
            }
        },
        singleColor:{
            type: Boolean,
            default: function(){
                return false;
            }
        },
        title: {
            type: String
        },
        tooltip:{
            type: Boolean,
            default: function(){
                return false;
            }
        }
    },
    mounted(){
        this.drawLine();
    },
    created(){
        this.pieData.forEach(item => {
            this.total += item.value
        });
    },
    methods:{
        drawLine(){
            let _this = this
            // 基于准备好的dom，初始化echarts实例
            this.myChart = this.$echarts.init(document.getElementById(this.id));
            this.myChart.setOption({                
                title:{
                    show: _this.title ? true : false,
                    text: _this.title ? _this.title : '',
                    left: 'center',
                    top: 'middle',
                    textStyle:{
                        color: '#fafafa',
                        fontWeight: 100
                    }                    
                },
                color: _this.singleColor ? 'rgba(228, 233, 220, 0.9)' : ['#FF6B6A', '#FF9FF4', '#48DBFC', '#1CD0A1', '#FDC958', '#FFAD76'],
                tooltip: {
                    show: _this.tooltip,
                    trigger: 'item',
                    // formatter: function(p){                        
                    //     // 故意不填可用來觸發HighLight
                    // }
                },
                series: [
                    {
                        type:'pie',
                        hoverOffset: 5,
                        radius: _this.singleColor ? ['30%', '70%'] : ['50%', '70%'],
                        avoidLabelOverlap: false,
                        label: {
                            normal: {
                                show: false,
                            },
                            emphasis: {
                                show: false,
                            }
                        },
                        labelLine: {
                            normal: {
                                show: false
                            }
                        },
                        data: _this.pieData,
                        itemStyle: _this.singleColor ? {
                            emphasis: {
                                color: '#1CD0A1',
                            }
                        } : ''
                    }
                ]
            });
            // mouseover觸發項
            this.myChart.on('mouseover', function (params) {
                if(_this.pieData[_this.heightlightIndex].name != params.name){
                    _this.myChart.dispatchAction({
                        type: 'downplay',
                        dataIndex: _this.heightlightIndex,
                    });
                }
                _this.$emit('highLightInfo', params);
            });
            if(this.defaultActive){
                // 預設先給第一筆初始值
                let now = this.pieData[0].value;
                let params = {'value': this.pieData[0].value, 'name': this.pieData[0].name,'percent': Number((now/this.total)*100).toFixed(2)}
                this.$emit('extraInfo', params);            
                this.$emit('highLightInfo', params);  
                // 預設第一個
                this.myChart.dispatchAction({
                    type: 'highlight',
                    dataIndex: _this.heightlightIndex,
                });
            }
        },
        // 供外部呼叫用
        heightlight: function(dName){    
            let _this = this;
            if(this.pieData[this.heightlightIndex].name != dName){
                this.myChart.dispatchAction({
                    type: 'downplay',
                    dataIndex: _this.heightlightIndex,
                });
            }

            this.myChart.dispatchAction({
                type: 'highlight',
                name: dName
            });
            let now = 0;
            this.pieData.forEach(item => {
                if(item.name == dName){
                    now = item.value
                }
            });
            let params = {'percent': Number((now/this.total)*100).toFixed(2)}


            this.$emit('extraInfo', params);
        },
        downplay: function(dname){
            this.myChart.dispatchAction({
                type: 'downplay',
                name: dname
            });
        }
    }
}

</script>

<style>
</style>
