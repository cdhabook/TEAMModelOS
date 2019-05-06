<style lang="less" scoped>
  @import '../../../assets/CSS/student-analysis/common.css';
</style>
<template>
  <Row class="index">
    <Headers :parentToChild="satitle" :identityselect="identitydata"></Headers>
    <Row class="index_box">
      <Col span="24" class="content_box">
      <div class="index_box_title"><p>学情分析服务</p></div>
      <Row class="content">
        <Col class="left_box" style="width:14%;float:left">
        <ChooseAttention></ChooseAttention>
        <ChooseExam></ChooseExam>
        </Col>
        <Col span="20" class="right_box" style="width:86%;float:left">
      
        <Col class="left_echarts">
        
        </Col>
        <Col class="center_echarts">
      
        </Col>
        <Col class="right_echarts">
       
        </Col>
        </Col>
        <Col span="20" class="bottom_linechart">
      
        </Col>
      </Row>
      </Col>
    </Row>
  </Row>
</template>

<script>
  import Headers from '@/common/headers.vue'
  import ChooseAttention from '@/components/student-analysis/SAchooseattention.vue'
  import ChooseExam from '@/components/student-analysis/SAchooseexam.vue'
    export default {
      components:{
        Headers,
        ChooseAttention,
        ChooseExam,
      },
      data(){
          return{
            satitle: '学情分数分析服务/学情仪表板',
            identitydata: [
              { "id": 1, "name": '成都紫藤小学', "rolename": '管理员', status: '1' },
              { "id": 2, "name": '成都七中小学', "rolename": '班主任', status: '2' }
            ],
                }
      },
      mounted(){
        this.init();
        this.ceshiApi();
      },
      methods: {
        init(){
        },
        ceshiApi(){
          //this.$get('/api/v2/movie/top250')
          //  .then((response) => {
          //    console.log(response,1111111)
          //  })
        }
      },
    }
</script>
<style>
  body {
    margin: 0px !important;
    font-family: "微软雅黑";
  }
</style>


