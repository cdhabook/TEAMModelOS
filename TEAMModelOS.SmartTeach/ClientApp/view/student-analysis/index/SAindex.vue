<style lang="less" scoped>
 @import '../../../assets/CSS/student-analysis/common.css';
 @import './SAindex.css';
</style>
<template>
    <Row  class="index">
     <Headers :parentToChild="satitle" :identityselect="identitydata" ></Headers>
    <Row class="index_box">
     <Col span="24" class="content_box">
      <div class="index_box_title"><p>学情分析服务</p></div>
      <Row class="content">
        <Col class="left_box" style="width:14%;float:left">
            <ChooseAttention></ChooseAttention>
            <ChooseExam></ChooseExam>
        </Col>
        <Col  class="right_box" style="width:86%;float:left">
          <Basics></Basics>
          <Col class="left_echarts">
            <Bargraph></Bargraph>
          </Col>
          <Col class="center_echarts">
            <Radargraph></Radargraph>
          </Col>
          <Col class="right_echarts">
            <Subjectsmanifestation></Subjectsmanifestation>
          </Col>
        </Col>
        <Col  span="20" class="bottom_linechart">
          <LineChart></LineChart>
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
  import Basics from '@/components/student-analysis/SAbasics.vue'
  import Bargraph from '@/components/student-analysis/SAbargraph.vue'
  import Radargraph from '@/components/student-analysis/SAradargraph.vue'
  import Subjectsmanifestation from '@/components/student-analysis/SAsubjectsmanifestation.vue'
  import LineChart from '@/components/student-analysis/SAlinechart.vue'
    export default {
      components:{
        Headers,
        ChooseAttention,
        ChooseExam,
        Basics,
        Bargraph,
        Radargraph,
        Subjectsmanifestation,
        LineChart
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
          //柱状图
          //let yuwen = ["103","110","108","99","103","110","120"];
          //let shuxue = ["98","100","105","109","85","90","103"];
          //let yingyu = ["110","102","103","100","99","93","83"];
          //let wuli = ["77","80","75","69","80","77","82"];
          //let huaxue = ["63","75","78","81","90","72","75"];
          //let shengwu = ["68","70","72","78","74","76","79"];
          //let quyu = ["526","526","526","526","526","526","526"];
          //let object= ["成都七中","成都四中","成都九中","成都树德中学","师大一中","西川中学","成都市石室中学"];
          //let name=["语文","数学","英语","物理","化学","生物"];
          //this.$store.state.zhuxhuang.titledata = name;
          //this.$store.state.zhuxhuang.object_name = object;
          //this.$store.state.zhuxhuang.languagedata = yuwen;
          //this.$store.state.zhuxhuang.mathdata = shuxue;
          //this.$store.state.zhuxhuang.englishdata = yingyu;
          //this.$store.state.zhuxhuang.physicsdata = wuli;
          //this.$store.state.zhuxhuang.chemistrydata = huaxue;
          //this.$store.state.zhuxhuang.biologydata = shengwu;
          //this.$store.state.zhuxhuang.area_score = quyu;
          //this.$store.state.zhuxhuang.mode = true;

          //雷达图
          //let mingcheng=["校级数据","区级数据"];
          //let dataname=["校级 VS 区域"];
          //let data1=["103","110","101","82","78","66"];
          //let data2=["99","103","108","88","69","79"];
          //this.$store.state.leida.titledata = mingcheng;
          //this.$store.state.leida.dataname = dataname;
          //this.$store.state.leida.data1 = data1;
          //this.$store.state.leida.data2 = data2;
          //this.$store.state.leida.mode = true;


          //饼图
          //let one=["80"];
          //let one_num=["20"];
          //let two=["54"];
          //let two_num=["46"];
          //let three=["72"];
          //let three_num=["28"];
          //let four=["66"];
          //let four_num=["34"];
          //let five=["60"];
          //let five_num=["40"];
          //let six=["82"];
          //let six_num=["18"];
          //this.$store.state.pie.one=one;
          //this.$store.state.pie.one_num=one_num;
          //this.$store.state.pie.two=two;
          //this.$store.state.pie.two_num=two_num;
          //this.$store.state.pie.three=three;
          //this.$store.state.pie.three_num=three_num;
          //this.$store.state.pie.four=four;
          //this.$store.state.pie.four_num=four_num;
          //this.$store.state.pie.five=five;
          //this.$store.state.pie.five_num=five_num;
          //this.$store.state.pie.six=six;
          //this.$store.state.pie.six_num=six_num;
          //this.$store.state.pie.mode = true;

          //折线图
          //this.$store.state.linechart.mode = true;
          //let line_name=["成都七中","成都九中","成都树德","川师附中","成都四中"];
          //let line_data=["2019第一次区级联考","2019第二次区级联考","2019第三次区级联考","2019第四次区级联考","2019第五次区级联考"];
          //let line_series=[
          //                  {"name":'成都七中',"data":["580","610","530","490","550"]},
          //                  {"name":'成都九中',"data":["530","620","550","500","520"]},
          //                  {"name":'成都树德',"data":["560","600","540","530","510"]},
          //                  {"name":'川师附中',"data":["520","580","530","510","520"]},
          //                  {"name":'成都四中',"data":["540","605","548","515","540"]},
          //                ];
          //this.$store.state.linechart.obj_name=line_name;
          //this.$store.state.linechart.obj_data=line_data;
          //this.$store.state.linechart.obj_series=line_series;

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
  body{
    margin: 0px !important;
    font-family: "微软雅黑";
  }
</style>
