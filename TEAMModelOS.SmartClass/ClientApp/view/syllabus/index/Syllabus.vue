
<template>
  <div class="syllabus-main center">
    <div class="content-wrap">
      <div class="header-wrap">
        <Input placeholder="关键词搜索课纲" style="width:20.2% !important;" />
        <div class="header-right">
          <div class="header-right-item" @click="addSubject">
            <Icon type="logo-buffer" size="18" />
            <span>新增科目</span>
          </div>
          <div class="header-right-item" @click="addTerm" v-if="currentSubjectList.length > 0">
            <Icon type="ios-browsers" size="18" />
            <span>新增册别/课纲</span>
          </div>
          <div class="header-right-item">
            <Icon type="ios-cloud-download" size="18" />
            <span>导入课纲</span>
          </div>
        </div>
      </div>
      <div class="list-wrap">
        <!-- 左边科目列表 -->
        <div class="list-col list-left">
          <Spin fix v-show="subjectLoading"></Spin>
          <template v-if="currentSubjectList.length > 0">
            <div v-for="item in currentSubjectList" :class='["subject-item", item.rowKey==s_click_id ? "term-item-active" : ""]' @click="subjectClick(item)">
              <span class="subject-name">
                <Input v-if="item.rowKey==c_edit_id" clearable type="text" v-model="item.name"
                       @on-blur="editSubject(item)"
                       @keyup.enter.native="$event.target.blur"
                       @click.stop.native="inputFocus($event)" />
                <span v-else>{{item.name}}</span>

                <span class="s-block-tools" v-show="item.rowKey==c_subject_id">
                  <Icon type="md-create" @click="handleEditSubject(item,$event)" />
                  <Icon type="md-trash" @click="handleDeleteSubject(item)" />
                </span>
              </span>
              <span class="subject-term-num">册数 | {{item.termNum?item.termNum:0}}</span>
            </div>
          </template>
          <div v-else class="no-data-text">暂无数据</div>
        </div>
        <!-- 中间册别列表 -->
        <div class="list-col list-middle">
          <Spin fix v-show="volumesLoading"></Spin>
          <template v-if="volumesList.length > 0">
            <div v-for="item in volumesList" :class='["subject-item", "term-item", item.rowKey==c_click_id ? "term-item-active" : ""]' @click="volumesClick(item)">
              <span class="subject-name term-name">
                <Input v-if="item.rowKey==t_edit_id" clearable type="text" v-model="item.name"
                       @on-blur="editTerm(item)"
                       @keyup.enter.native="$event.target.blur"
                       @click.stop.native="inputFocus($event)" />
                <span v-else class="term-name-title">{{item.name}} | {{item.termCode}}</span>
                <span class="s-block-tools" v-show="item.rowKey==c_term_id">
                  <Icon type="md-create" @click="handleEditTerm(item,$event)" />
                  <Icon type="md-trash" @click="handleDeleteVolumes(item)" />
                </span>
              </span>
              <span class="term-period">{{item.periodName}} | {{item.gradeName + item.termName}}  <Icon type="md-arrow-dropdown" v-show="item.rowKey==c_click_id" size="18" style="margin-bottom:3px" /></span>
              <div class="term-userNum">
                <span>共编使用者数 | {{item.termCode}} 名</span>
                <Poptip content="content" placement="bottom-end" theme="dark">
                  <span class="term-add-user" v-show="item.id==c_click_id">添加共编使用者</span>
                  <div class="pop-content" slot="content" style="padding-bottom:10px;">
                    <Tabs value="filter" :animated="false">
                      <TabPane label="选择老师" name="filter">
                        <template>
                          <Select v-model="selectGrade" style="width:100%" @on-change="handleGradeChange">
                            <Option v-for="item in gradeList" :value="item.name" :key="item.name">{{ item.name }}</Option>
                          </Select>
                        </template>
                        <CheckboxGroup v-model="tearchList" @on-change="handleTeacherCheck">
                          <Checkbox v-for="(item,index) in userList" :key="index" :label="item.index">{{item.name}}{{item.role}}</Checkbox>
                        </CheckboxGroup>
                      </TabPane>
                      <TabPane label="选择群组" name="orderBy">

                      </TabPane>
                    </Tabs>
                  </div>
                </Poptip>
              </div>
              <!-- 使用者列表 -->
              <transition enter-active-class="animated slideInDown" leave-active-class="d-none">
                <div class="term-users" v-show="item.id==c_click_id">
                  <div class="term-user-item center" v-for="user in userList">
                    <img class="term-user-img" :src="user.headImg" />
                    <span class="term-user-name">{{user.name}} {{user.role}}</span>
                  </div>
                </div>
              </transition>
            </div>
          </template>
          <div v-else class="no-data-text">暂无数据</div>
        </div>
        <!-- 右侧树形列表 -->
        <div class="list-col list-right">
          <Spin fix v-show="syllabusLoading"></Spin>
          <Tree :treeDatas="treeDatas" :volumeCode="volumeCode" v-if="volumesList.length > 0 && treeDatas.length > 0"></Tree>
          <div v-else class="no-data-text">暂无数据</div>
        </div>
      </div>

      <!-- 新增科目弹窗  -->
      <Modal v-model="addSubjectModal"
             title="新增科目"
             class-name="subject-modal"
             ok-text="确认"
             cancel-text="取消"
             @on-ok="handleAddSubject"
             @on-cancel="">
        <p class="modal-title" v-show="currentSubjectList.length > 0">当前已选科目</p>
        <CheckboxGroup v-model="currentSubjectList"  v-show="currentSubjectList.length > 0">
          <Checkbox v-for="(item,index) in currentSubjectList" :label="index" :key="index" disabled>{{item.name}}</Checkbox>
        </CheckboxGroup>

        <p class="modal-title">请选择需要添加的新科目</p>
        <CheckboxGroup v-model="newSubjectList">
          <Checkbox v-for="(item,index) in subjectList" :label="index" :key="index">{{item.name}}</Checkbox>
        </CheckboxGroup>
      </Modal>

      <!-- 新增册别弹窗 -->
      <Modal v-model="addTermModal"
             title="新增册别"
             class-name="subject-modal"
             ok-text="确认"
             cancel-text="取消"
             @on-ok="handleAddTerm"
             @on-cancel="">
        <p class="modal-title">当前科目：{{subjectInfo.name}}</p>
        <p class="modal-title">请选择学段：</p>
        <Select v-model="periodSelect" style="width:200px" @on-change="periodChange" label-in-value>
          <Option v-for="item in findPeriodsList" :value="item.code" :key="item.code">{{ item.name }}</Option>
        </Select>
        <p class="modal-title">请选择年级：</p>
        <Select v-model="gradeSelect" style="width:200px" @on-change="gradeChange" label-in-value>
          <Option v-for="item in findGradesList" :value="item.code" :key="item.code">{{ item.name }}</Option>
        </Select>
        <p class="modal-title">请选择学期：</p>
        <Select v-model="termSelect" style="width:200px" @on-change="termChange" label-in-value>
          <Option v-for="item in findTermsList" :value="item.code" :key="item.code">{{ item.name }}</Option>
        </Select>
        <p class="modal-title">请输入册别名称：</p>
        <Input v-model="addVolumesData.name" placeholder="输入新册别名称" style="width: 300px" />
      </Modal>
    </div>
  </div>
</template>

<script>
  import Tree from '@/components/syllabus/Tree.vue'
import { all } from 'q';
import { setTimeout } from 'core-js';
  export default {
    name: "Syllabus",
    components: {
      Tree: Tree
    },
    data() {
      return {
        schoolInfos: {}, //身份全部信息
        schoolInfo: {},//学校信息
        subjectInfo: {},//当前选中科目信息
        subjectList: [],//全部科目列表
        currentSubjectList: [],//学校科目列表
        volumesList: [],//册别列表
        treeData: [],//课纲数据
        userList: [],//共编使用者列表
        treeDatas: [],
        volumeCode: "",
        subjectLoading: true,
        volumesLoading: true,
        syllabusLoading:true,

        findPeriodsList:[],
        findGradesList:[],
        findTermsList: [],
        newVolumesName:"",
        periodSelect:"",
        gradeSelect:"",
        termSelect: "",
        addVolumesData: {},

        gradeList: [],
        tearchList: [],
        c_subject_id:0,
        c_term_id: 0,
        c_click_id: 0,
        s_click_id: 0,
        c_edit_id: null,
        t_edit_id:null,
        addSubjectModal: false,
        addTermModal: false,
        newSubjectList: [],
        termName: "",
        selectGrade: "1年级",

      }
    },
    created() {
      this.schoolInfo = JSON.parse(localStorage.getItem('c_role_info')).roleClaim[0]; //默认选中第一个学校
      let schoolClaims = this.schoolInfo.claim;
      for (let i in schoolClaims) {
        if (schoolClaims[i].claimType == "SchoolCode") {
          this.schoolInfo = schoolClaims[i]
        }
      }

      this.findAllSubjects();
      this.findSchoolSubjectsByDict();

    },
    methods: {
      //查找全部科目
      findAllSubjects() {
        this.$api.FindSubjectsByDict({}).then(res => {
          this.subjectList = res.result.data;
        })
      },
      //查找当前学校已有科目
      findSchoolSubjectsByDict() {
        this.$api.FindSchoolSubjectsByDict({ SchoolCode: this.schoolInfo.claimCode,Status:1 }).then(res => {
          this.currentSubjectList = res.result.data;
          if (res.result.data.length > 0) {
             this.subjectInfo = res.result.data[0];
             this.subjectClick(res.result.data[0]);
          }
          this.subjectLoading = false;
          this.volumesLoading = false;
          this.syllabusLoading = false;
        })
      },
       //点击科目
      subjectClick(data) {
        this.volumesLoading = true;
        this.subjectInfo = data;
        this.s_click_id = data.rowKey;
        this.c_subject_id = data.rowKey;
        this.c_edit_id = null;
        let defaultData = {
          SchoolCode: this.schoolInfo.claimCode,
          PartitionKey: data.partitionKey,
          SubjectCode: data.code,
          Status:1
        }
        this.$api.FindSchoolVolumesByDict(defaultData).then(res => {
          this.volumesList = res.result.data;
          if (res.result.data.length > 0) {
            this.volumesClick(res.result.data[0]);
          }
          this.volumesLoading = false;
         })
      },

      //点击编辑科目
      handleEditSubject(data, e) {
        e.stopPropagation();
        this.c_edit_id = data.rowKey;
      },

      //点击册别
      volumesClick(data) {
        this.syllabusLoading = true;
        this.$api.FindSyllabusByVolumeCode({ VolumeCode: data.rowKey, Status:1 }).then(res => {
          this.treeDatas = res.result.data;
          this.volumeCode = data.rowKey;
          this.syllabusLoading = false;
        })
        this.c_click_id = data.rowKey;
        this.c_term_id = data.rowKey;
        this.t_edit_id = null;
      },

      //点击编辑册别
      handleEditTerm(data, e) {
        e.stopPropagation();
        this.t_edit_id = data.rowKey;
      },
      //编辑科目input失焦
      editSubject(data) {
        this.$api.SaveOrUpdateSchoolSubject(data).then(res => {
            if (res.result.message == "Success") {
              this.$Message.success("修改成功");
            }
          })
        this.c_edit_id = null;
      },
      //编辑册别input失焦
      editTerm(data) {
          this.$api.SaveOrUpdateSchoolVolume(data).then(res => {
            if (res.result.message == "Success") {
              this.$Message.success("修改成功");
            }
          })
        this.t_edit_id = null;
      },
      //input聚焦
      inputFocus(e) {
        //e.cancelBubble = true;
      },
      //点击添加科目
      addSubject() {
        let currentList = this.currentSubjectList;
        this.$api.FindSubjectsByDict({}).then(res => {
          this.subjectList = res.result.data;
          let tempList = this.subjectList.slice(0);
          if (currentList.length > 0) {
            for (let i in tempList) {
              for (let j in currentList) {
                if (tempList[i].rowKey == currentList[j].code) {
                  this.subjectList.splice(this.subjectList.indexOf(tempList[i]), 1);
                }
              }
            }
          }
        })


        this.addSubjectModal = true;
        this.newSubjectList = [];

      },

      //确认添加科目
      handleAddSubject() {
        let newList = this.newSubjectList;
        for (let i of newList) {
          let defaultSubject = {
            schoolCode: this.schoolInfo.claimCode,
            partitionKey: this.subjectList[i].partitionKey,
            name: this.subjectList[i].name,
            code: this.subjectList[i].rowKey,
            status:1,
            schoolName:this.schoolInfo.claimName
          };
           this.$api.SaveOrUpdateSchoolSubject(defaultSubject).then(res => {
             this.currentSubjectList.push(res.result.data);
             this.subjectClick(this.currentSubjectList[0]); //默认展开第一个
           })
        }
      },

      //点击添加册别
      addTerm() {
        this.addTermModal = true;
        this.findSchoolPeriodsByDict();
        this.findSchoolTermsByDict();
        this.addVolumesData =  {
            schoolCode: this.schoolInfo.claimCode,
            partitionKey: this.schoolInfo.partitionKey,
            schoolName: this.schoolInfo.claimName,
            subjectName: this.subjectInfo.name,
            subjectCode: this.subjectInfo.code,
            status: 1,
            periodName: 0,
            gradeName: 0,
            termName: 0,
            periodCode: 0,
            gradeCode: 0,
            termCode: 0,
            name:this.newVolumesName
          }
      },

      //发送请求添加册别
      handleAddTerm() {
        //查询是否已存在此类册别
        this.$api.FindSchoolVolumesByDict({
          SchoolCode: this.addVolumesData.schoolCode,
          PartitionKey: this.addVolumesData.partitionKey,
          SubjectCode: this.addVolumesData.subjectCode,
          Status: this.addVolumesData.status,
          PeriodCode: this.addVolumesData.periodCode,
          GradeCode: this.addVolumesData.gradeCode,
          TermCode: this.addVolumesData.termCode,
        }).then(res => {
          if (res.result.data.length > 0) {
            this.$Modal.confirm({
              title: '温馨提示',
              content: '<p>已存在该学期下的册别，是否覆盖？</p>',
              okText: "确认",
              cancelText:"取消",
              onOk: () => {
                //存在即更新册别名字
                this.$api.SaveOrUpdateSchoolVolume(this.addVolumesData).then(res => {
                  this.c_term_id = res.result.data.rowKey;
                  this.c_click_id = res.result.data.rowKey;
                  for (let i in this.volumesList) {
                    if (this.volumesList[i].rowKey == res.result.data.rowKey) {
                      this.volumesList[i].name =  res.result.data.name
                    }
                  }
                })
              }
            });
          } else {
            //不存在则直接加入列表
            this.$api.SaveOrUpdateSchoolVolume(this.addVolumesData).then(res => {
              this.volumesList.push(res.result.data);
              this.c_term_id = res.result.data.rowKey;
              this.c_click_id = res.result.data.rowKey;
            })
          }
        })
      },

      //删除科目，修改科目状态
      handleDeleteSubject(data) {
        data.status = 0;
        this.$Modal.confirm({
          title: '删除科目',
          content: '<p>确认删除该科目？</p>',
          okText: "确认",
          cancelText:"取消",
          onOk: () => {
            this.$api.SaveOrUpdateSchoolSubject(data).then(res => {
              data.status = 1;
              this.$Message.success("删除成功");
              this.currentSubjectList.splice(data, 1);
              this.subjectList.push(data);
              this.findSchoolSubjectsByDict();
              if (this.currentSubjectList.length == 0) {
                this.volumesList = [];
                this.treeData = [];
              }
           })
          },
          onCancel: () => {
            this.$Message.info('Clicked cancel');
          }
        });
      },

      //删除册别，修改册别状态
      handleDeleteVolumes(data) {
        data.status = 0;
        this.$Modal.confirm({
          title: '删除科目',
          content: '<p>确认删除该册别？</p>',
          okText: "确认",
          cancelText:"取消",
          onOk: () => {
            this.$api.SaveOrUpdateSchoolVolume(data).then(res => {
              this.$Message.success("删除成功");
              this.subjectClick(this.subjectInfo);
           })
          },
          onCancel: () => {
            this.$Message.info('Clicked cancel');
          }
        });
      },
      //获取当前学校全部学段
      findSchoolPeriodsByDict() {
        this.$api.FindSchoolPeriodsByDict({SchoolCode: this.schoolInfo.claimCode}).then(res => {
          this.findPeriodsList = res.result.data;
          this.periodSelect = res.result.data[0].code;
          this.addVolumesData.periodCode = res.result.data[0].code;
          this.addVolumesData.periodName = res.result.data[0].name;
          this.findSchoolGradesByDict(res.result.data[0].code);
        })
      },
       //获取当前学校全部年级
      findSchoolGradesByDict(periodCode) {
        let data = {
          SchoolCode: this.schoolInfo.claimCode,
          PeriodCode: periodCode
        }
        this.$api.FindSchoolGradesByDict(data).then(res => {
          this.findGradesList = res.result.data;
          this.gradeSelect = res.result.data[0].code;
          this.addVolumesData.gradeCode = res.result.data[0].code;
          this.addVolumesData.gradeName = res.result.data[0].name;
        })
      },
      //获取当前学校全部学期
      findSchoolTermsByDict() {
        this.$api.FindSchoolTermsByDict({SchoolCode: this.schoolInfo.claimCode}).then(res => {
          this.findTermsList = res.result.data;
          this.termSelect = res.result.data[0].code;
          this.addVolumesData.termCode = res.result.data[0].code;
          this.addVolumesData.termName = res.result.data[0].name;
        })
      },

      periodChange(val) {
        console.log(val);
        this.addVolumesData.periodName = val.label;
        this.addVolumesData.periodCode = val.value;
      },
      gradeChange(val) {
        console.log(val);
        this.addVolumesData.gradeName = val.label;
        this.addVolumesData.gradeCode = val.value;
      },
      termChange(val) {
        console.log(val);
        this.addVolumesData.termName = val.label;
        this.addVolumesData.termCode = val.value;
      },

      //选择年级的自动完成
      filterGrade(value, option) {
        return option.toUpperCase().indexOf(value.toUpperCase()) !== -1;
      },

      //选择老师的回调事件
      handleTeacherCheck() {
        console.log(this.tearchList);
      },

      //选择老师的回调事件
      handleGradeChange(val) {
        console.log(val);
      }
    }
  }
</script>



