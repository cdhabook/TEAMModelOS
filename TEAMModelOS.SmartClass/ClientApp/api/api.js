import { fetch, post } from '@/filters/http'
export default {
  //获取登录跳转链接
  getLoginLink: function (data) {
     return post('api/login/login', data);
  },
  //验证登录
  checkLogin: function (data) {
     return post('api/login/CheckLogin', data);
  },
  //获取登录人员角色列表
  getLoginRoles: function (data) {
    return post('api/role/GetLoginRoles', data);
  },
  //查找地区对应学校列表
  getSchoolList: function (data) {
    return post('api/School/getSchool', data);
  },
  //根据当前登录用户获取已授权的AI智慧学校
  getAuthSchool: function (data) {
    return post('api/School/AuthorizedAISchool', data);
  },
  //获取全部科目
  FindSubjectsByDict: function (data) {
    return post('api/subject/FindSubjectsByDict', data);
  },
  //获取当前学校全部科目
  FindSchoolSubjectsByDict: function (data) {
    return post('api/subject/FindSchoolSubjectsByDict', data);
  },
  //获取当前学校全部册别
  FindSchoolVolumesByDict: function (data) {
    return post('api/volume/FindSchoolVolumesByDict', data);
  },
  //获取当前学校全部学段
  FindSchoolPeriodsByDict: function (data) {
    return post('api/period/FindSchoolPeriodsByDict', data);
  },
  //获取当前学校全部年级
  FindSchoolGradesByDict: function (data) {
    return post('api/grade/FindSchoolGradesByDict', data);
  },
  //获取当前学校全部学期
  FindSchoolTermsByDict: function (data) {
    return post('api/term/FindSchoolTermsByDict', data);
  },
  //保存或更新学校科目
  SaveOrUpdateSchoolSubject: function (data) {
    return post('api/subject/SaveOrUpdateSchoolSubject', data);
  },
  //保存或更新学校册别
  SaveOrUpdateSchoolVolume: function (data) {
    return post('api/volume/SaveOrUpdateSchoolVolume', data);
  },
  //根据册别及其他条件获取课纲树形结构
  FindSyllabusByVolumeCode: function (data) {
    return post('api/syllabus/FindSyllabusByVolumeCode', data);
  },
  //保存单个课纲节点
  SaveOrUpdateSingleNode: function (data) {
    return post('api/Syllabus/SaveOrUpdate', data);
  },





  //获取登录人员身份信息
  getLoginClaim: function (data) {
    return post('api/role/GetLoginClaim', data);
  },











  //学情分析API
  //查询班年级数据
  FindGrade: function () {
    return fetch('/api/class/getGrade?identity=Grade');
  },
  //查询学年期数据
  FindTerm: function () {
    return fetch('/api/class/getTerm?identity=term');
  },
  //查询考试情况
  FindExam: function () {
    return fetch('/api/class/getExam?identity=Exam');
  },
  //查询基础数据
  FindBasics: function () {
    return fetch('/api/class/getBase?identity=Base');
  },
  //查询各校成绩排名（堆叠柱状图）
  FindBargraph: function () {
    return fetch('/api/class/getExam?identity=EcharsZ');
  },
  //查询各项科目表现（雷达图）
  FindRadargraph: function () {
    return fetch('/api/class/getExam?identity=EcharsL');
  },
  //查询科目表现对比（饼图）
  FindSubjectsManifestation: function () {
    return fetch('/api/class/getExam?identity=EcharsY');
  },
  //查询历次总分统计表（折线图）
  FindLinechart: function () {
    return fetch('/api/class/getExam?identity=EcharsZx');
  },
  //查询考试类型
  FindExamtype: function () {
    return fetch('/api/class/getExam?identity=ExamType');
  },


  //动态数据
  //关注年级变化数据
  FindSelectGrade:function() {
    return fetch('/api/class/getChange?identity=Changegrade');
  },
 //关注学年期变化数据
  FindSelectTerm: function () {
    return fetch('/api/class/getChangeterm?identity=Changeterm');
  },
  //点击某次考试详情 变化数据
   FindSelectExam: function () {
     return fetch('/api/class/getChangeExam?identity=Changeexam');
  },
  //筛选考试类型 变化数据
  FindSelectExamType: function () {
    return fetch('/api/class/getChangeExamType?identity=Changeexamtype');
  },
  //显示文科数据  变化数据
  FindSelectArts: function () {
    return fetch('/api/class/getChangeArts?identity=Changarts');
  },
  //显示理科数据  变化数据
  FindSelectScience: function () {
    return fetch('/api/class/getChangeScience?identity=Changscience');
  },
  //点击柱状图 赋值到雷达图
  //成都七中
  Findcdqz: function () {
    return fetch('/api/class/getExam?identity=cdqz');
  },
  //成都四中
  Findcdsz: function () {
    return fetch('/api/class/getExam?identity=cdsz');
  },
  //成都九中
  Findcdjz: function () {
    return fetch('/api/class/getExam?identity=cdjz')
  },
  //成都树德
  Findcdsd: function () {
    return fetch('/api/class/getExam?identity=cdsd')
  },
  //师大一中
  Findsdyz: function () {
    return fetch('/api/class/getExam?identity=sdyz')
  },
  //西川中学
  Findxczx: function () {
    return fetch('/api/class/getExam?identity=xczx');
  },
  //石室中学
  Findsszx: function () {
    return fetch('/api/class/getExam?identity=sszx');
  },

  //任教老师页面
  //查询关注学年期
  FindTeachTerm: function () {
    return fetch('/api/class/getTerm?identity=Teachterm');
  },
  //查询数据对比
  FindTeachContrast: function () {
    return fetch('/api/class/getTerm?identity=Teachcontrast');
  },
  //查询考试信息
  FindTeachExam: function () {
    return fetch('/api/class/getExam?identity=TeachExam');
  },
  //查询基础数据
  FindTeachBasics: function () {
    return fetch('/api/class/getTerm?identity=TeachBase');
  },
  //查询各班平均分数排名（柱状图）
  FindTeachbargraph: function () {
    return fetch('/api/class/getExam?identity=TeachEcharsZ');
  },
  //查询及格率比较
  FindTeachAnnulus: function () {
    return fetch('/api/class/getExam?identity=TeachAnnulus');
  },
  //查询历次总分统计图
  FindTeachHistory: function () {
    return fetch('/api/class/getExam?identity=TeachEcharsZx');
  },
  //查询PR值
  FindTeachPR: function () {
    return fetch('/api/class/getExam?identity=TeachPie');
  },


  //动态数据
  //选择关注学年期
  FindDynamicTerm: function () {
    return fetch('/api/class/getExam?identity=TeachChangeterm');
  },
  //选择数据比较
  FindDatacompare: function () {
    return fetch('/api/class/getExam?identity=TeachChangeDatacompare');
  },
  //选择考试数据
  FindClickExam: function () {
    return fetch('/api/class/getExam?identity=TeachChangeExam');
  },
  //点击班级 联动 RP值
  FindClickPR: function () {
    return fetch('/api/class/getExam?identity=TeachClickPR');
  },
}
