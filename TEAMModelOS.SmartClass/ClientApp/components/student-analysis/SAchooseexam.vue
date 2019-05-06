<template>
   <Row class="exam_box">
     <Col span="24">
       <li v-for="(item,index) in selectexam" class="list_box" @click="select_box(index)">
         <p class="box_title">{{item.title}}-{{item.type}}</p>
         <p class="box_time">{{item.time}}</p>
         <p class="box_type">诊断类型：{{item.typename}}</p>
         <p class="box_num">测试人数：{{item.num}}人</p>
       </li>
       <infinite-loading  @infinite="onInfinite" ref="infiniteLoading"></infinite-loading>
     </Col>
   </Row>
</template>
<script>
  import InfiniteLoading from 'vue-infinite-loading';
  export default {
    components: {
      InfiniteLoading,
    },
    data() {
      return {
        list: [],
        now_url: this.$route.path,
        ops: {
          vuescroll: {},
          scrollPanel: {},
          rail: {
            keepShow: true
          },
          bar: {
            hoverStyle: true,
            onlyShowBarOnScroll: false, //是否只有滚动的时候才显示滚动条
            background: 'red',
          }
        },
      };
    },
    created() {
      this.init();
    },
    computed: {
      selectexam() {
        return this.$store.state.selectexam
            },
            },
    methods: {
      onInfinite() {
        //setTimeout(() => {
        //  const temp = [];
        //  // let suiji=Math.ceil(Math.random()*10);
        //  for (let i = this.list.length + 1; i <= this.list.length + 3; i++) {
        //    temp.push(i);
        //  }
        //  this.list = this.list.concat(temp);
        //  this.$refs.infiniteLoading.$emit('$InfiniteLoading:loaded');
        //}, 300);
      },

      //点击考试获取详情
      select_box(value) {
        if (this.now_url == '/saindex') {
          this.$api.FindSelectExam({})
            .then((response) => {
              //console.log(response.result.data);
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
        } else if (this.now_url == '/teach') {
          this.$api.FindClickExam({})
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
      init() {
        if (this.now_url == '/saindex') {
          this.$api.FindExam({})
            .then((response) => {
              //console.log(response.result.data,11111);
              this.$store.state.selectexam = response.result.data[0].data;
              //console.log(this.list,333333)
            })
        } else if (this.now_url == '/teach') {
          this.$api.FindTeachExam({})
            .then((response) => {
              console.log(response.result.data, 45678999990000)
              this.$store.state.selectexam = response.result.data[0].data;
            })
        } else if (this.now_url == '/total') {
          this.$api.FindExam({})
            .then((response) => {
              this.$store.state.selectexam = response.result.data[0].data;
            })
        }
      }
    },
  };
</script>
