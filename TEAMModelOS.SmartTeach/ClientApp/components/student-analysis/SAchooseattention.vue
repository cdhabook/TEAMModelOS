<template>
  <Row class="attention_box">
    <Col span="24">
      <Sider ref="side1" v-model="isCollapsed">
        <Menu active-name="1-1" theme="dark" width="100%">
          <MenuItem name="1-1">
            <Icon type="ios-home-outline"/>
            <span>{{$t('saindex.index')}}</span>
          </MenuItem>
          <MenuItem name="1-2">
            <Icon type="ios-clipboard-outline"/>
            <span>{{$t('saindex.grade')}}</span>
          </MenuItem>
          <MenuItem name="1-3">
            <Icon type="md-checkmark-circle-outline"/>
            <span>{{$t('saindex.droppoint')}}</span>
          </MenuItem>
          <MenuItem name="1-4">
            <Icon type="ios-create-outline"/>
            <span>{{$t('saindex.examination')}}</span>
          </MenuItem>
          <MenuItem name="1-5">
            <Icon type="ios-medal-outline"/>
            <span>{{$t('saindex.knowledge')}}</span>
          </MenuItem>
        </Menu>
      </Sider>
    </Col>
    <Col span="24" class="choose">
    <div class="select">
      <p class="choose_box_tilte" v-if="this.now_url == '/saindex'">{{$t('sasidebar.class')}}</p>
      <Cascader :data="selectgrade" v-model="grade"  placeholder="请选择年级" @on-change="listchange" v-if="this.now_url != '/teach'"></Cascader>
      <p class="choose_box_tilte">{{$t('sasidebar.term')}}</p>
      <Cascader :data="selectterm" v-model="semester" placeholder="请选择年级" @on-change="listterm"></Cascader>
      <p class="choose_box_tilte" v-if="this.now_url == '/teach'">{{$t('sasidebar.compare')}}</p>
      <Select v-model="model1" style="width:200px"  placeholder="请选择比较的数据"  v-if="this.now_url == '/teach'"  @on-change="datacompare">
        <Option v-for="item in selectcontrast" :value="item.value" :key="item.value">{{ item.label }}</Option>
      </Select>
    </div>
    </Col>
  </Row>
</template>
<script>
  export default {
    name: "ChooseAttention",
    data() {
      return {
        isCollapsed: false,
        value1: [],
        grade: [],
        semester: [],
        cityList: [],
        model1:[],
        now_url:this.$route.path,
      }
    },
    created() {
      this.init();
      this.inits();
      this.contrast();
    },
    computed: {
      //  	命令发布监听
      selectgrade() {
        return this.$store.state.selectgrade
      },
      selectterm(){
        return this.$store.state.selectterm
      },
      selectcontrast() {
        return this.$store.state.selectcontrast
      },
    },
    methods: {
      //获取关注年级数据
      init(){
          this.$api.FindGrade({})
            .then((response) => {
              //console.log(response.result.data, 11111);
              this.$store.state.selectgrade = response.result.data;
              //console.log(this.$store.state.selectgrade, 888855)
            })
      },
      //获取关注学年期
      inits() {
        if (this.now_url == '/saindex') {
          this.$api.FindTerm({})
            .then((response) => {
              //console.log(response.result.data,11111);
              this.$store.state.selectterm = response.result.data
            })
        } else if (this.now_url == '/teach') {
          this.$api.FindTeachTerm({})
            .then((response) => {
              this.$store.state.selectterm = response.result.data;
            })
        } else if (this.now_url == '/total') {
          this.$api.FindTeachTerm({})
            .then((response) => {
              this.$store.state.selectterm = response.result.data;
            })
        } 
      },
      //获取数据比较
   contrast(){
    this.$api.FindTeachContrast({})
      .then((response) => {
        this.$store.state.selectcontrast=response.result.data
              })
          },
      //获取关注年级值
      listchange(value, selectedData) {
        let xueduan = value[0];
        let nianjinum = value[1];
        if (xueduan == 'gaozhong' && nianjinum != 1) {
          this.$store.state.wenli_show = true;
        } else {
          this.$store.state.wenli_show = false;
        }
        //console.log(value,33333);
        this.$api.FindSelectGrade({})
          .then((response) => {
            //console.log(response.result.data, 789789789);
            //获取新的年级选择
            this.$store.state.selectgrade = response.result.data.selectgrade
            //获取新的学年期
            this.$store.state.selectterm = response.result.data.term
            //获取新的考试次数情况
            this.$store.state.selectexam = response.result.data.exam[0].data;
            //获取新的基础信息
            this.$store.state.basicsdata = response.result.data.base;
            //获取新的各校成绩排名
            this.$store.state.zhuxhuang = response.result.data.barecharts;
            //获取新的各项科目表现
            this.$store.state.leida = response.result.data.leida;
            //获取新的科目表现对比
            this.$store.state.pie = response.result.data.pie
            //获取新的考试类型
            this.$store.state.examtype = response.result.data.examtype;
            //获取新的各校历次总分统计图
            this.$store.state.linechart = response.result.data.linechart;
                  })
      },
      //获取关注学年期
      listterm(value, selectedData) {
        if (this.now_url == '/saindex') {
          this.$api.FindSelectTerm({})
            .then((response) => {
              //console.log(response.result.data);
              //获取新的关注年级
              this.$store.state.selectgrade = response.result.data.selectgrade
              //获取新的学年期
              this.$store.state.selectterm = response.result.data.term
              //获取新的考试次数情况
              this.$store.state.selectexam = response.result.data.exam[0].data;
              //获取新的基础信息
              this.$store.state.basicsdata = response.result.data.base;
              //获取新的各校成绩排名
              this.$store.state.zhuxhuang = response.result.data.barecharts;
              //获取新的各项科目表现
              this.$store.state.leida = response.result.data.leida;
              //获取新的科目表现对比
              this.$store.state.pie = response.result.data.pie
              //获取新的考试类型
              this.$store.state.examtype = response.result.data.examtype;
              //获取新的各校历次总分统计图
              this.$store.state.linechart = response.result.data.linechart;
            })
        } else if (this.now_url =='/teach') {
          this.$api.FindDynamicTerm({})
            .then((response) => {
              //console.log(response.result.data,33333)
              //获取新的学年期
              this.$store.state.selectterm = response.result.data.term;
              //获取新的数据对比
              this.$store.state.selectcontrast = response.result.data.selectcontrast;
              //获取新的考试次数情况
              this.$store.state.selectexam = response.result.data.exam[0].data;
              //获取新的基础信息
              this.$store.state.basicsdata = response.result.data.base;
              //获取各班平均分数排名
              this.$store.state.barline = response.result.data.barecharts;
              //获取及格率比较
              this.$store.state.annulus = response.result.data.annulus;
              //获取PR百分等级
              this.$store.state.accuracyPR = response.result.data.accuracyPR;
              //获取历次总分统计图
              this.$store.state.linechart = response.result.data.linechart;
            })
        }
       
      },
      //动态数据比较
      datacompare(value, selectedData) {
        this.$api.FindDatacompare({})
          .then((response) => {
            //获取新的学年期
            this.$store.state.selectterm = response.result.data.term;
            //获取新的数据对比
            this.$store.state.selectcontrast = response.result.data.selectcontrast;
            //获取新的考试次数情况
            this.$store.state.selectexam = response.result.data.exam[0].data;
            //获取新的基础信息
            this.$store.state.basicsdata = response.result.data.base;
            //获取各班平均分数排名
            this.$store.state.barline = response.result.data.barecharts;
            //获取及格率比较
            this.$store.state.annulus = response.result.data.annulus;
            //获取PR百分等级
            this.$store.state.accuracyPR = response.result.data.accuracyPR;
            //获取历次总分统计图
            this.$store.state.linechart = response.result.data.linechart;
          })
      }
    },
  }
</script>
<style scoped>
  .ivu-select-selection {
    background-color: #cfd4da !important;
  }

  .ivu-input {
    font-size: 14px !important;
    background-color: #cfd4da;
    color: #4d5760 !important;
    border: 1px solid #cfd4da;
  }

  .ivu-input-wrapper {
    color: #000 !important;
  }

  .ivu-input:hover {
    border: 1px solid #fff;
  }
  .ivu-select {
    width: 100% !important;
  }
</style>
<style scoped>
</style>
