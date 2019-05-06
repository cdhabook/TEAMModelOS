<template>
  <div class="tree-main">

    <Tree :data="treeData" :render="renderContent" ref="tree"></Tree>
    <Modal v-model="modalFlag" title="添加新节点" ok-text="确认" cancel-text="取消" @on-ok="handleAddNode">
      <Row class="modelRow">
        <span>
          当前章节:
          <span style="margin-left:10px;">{{currentNode.title}}</span>
        </span>
      </Row>
      <Row class="modelRow">
        <span>章节类型：</span>
        <RadioGroup v-model="nodeType" type="button">
          <Radio label="章节"></Radio>
          <Radio label="课件"></Radio>
        </RadioGroup>
      </Row>
      <Row class="modelRow">
        <span>章节名称：</span>
        <Input v-model="inputValue" placeholder="输入新节点名称" style="width: 100%" />
      </Row>
    </Modal>
    <Modal v-model="editFlag" title="修改节点" ok-text="确认" cancel-text="取消" @on-ok="handleUpdateNode">
      <Row class="modelRow">
        <span>
          当前名称:
          <span style="margin-left:10px;">{{currentNode.title}}</span>
        </span>
      </Row>
      <Row class="modelRow">
        <span>章节类型：</span>
        <RadioGroup v-model="nodeType" type="button">
          <Radio label="章节"></Radio>
          <Radio label="课件"></Radio>
        </RadioGroup>
      </Row>
      <Row class="modelRow">
        <span>章节名称：</span>
        <Input v-model="editValue" placeholder="输入节点新名称" style="width: 100%" />
      </Row>
    </Modal>
  </div>
</template>
<script>
  export default {
    props:["treeDatas","volumeCode"],
    data() {
      return {
        bookSelect: "语文人教课标版四年级上册",
        periodList: [],
        subjectList: [],
        versionList: [],
        gradeList: [],
        slideGradeList: [],
        treeData: [],
        inputValue: "",
        editValue: "",
        modalFlag: false,
        editFlag: false,
        addBookFlag: false,
        nodeType: "章节",
        currentNode: "",
        currentLiNode: "",
        buttonProps: {
          type: "default",
          size: "small"
        },
        periodSelect: "",
        subjectSelect: "",
        versionSelect: "",
        gradeSelect: "",
        periodName: "",
        subjectName: "",
        versionName: "",
        gradeName: "",
      };
    },
    created() {
      this.treeData = this.treeDatas;
    },
    methods: {
      renderContent(h, { root, node, data }) {
        let that = this;
        return h(
          "span",
          {
            style: {
              display: "inline-block",
              width: "95%",
              textAlign: "left",
              paddingLeft: "2%",
              cursor: "pointer",
              position: "relative",
              boxSizing: "border-box"
            },
            domProps: {
              className: "singleClass"
            },
            on: {
              click: () => {
                this.titleClick(root, node, data, event);
              },
              mouseover: e => {
                e.stopPropagation();
                this.handleLiOver(event);
              },
              mouseleave: e => {
                e.stopPropagation();
                this.handleLiLeave(node, event);
              }
            }
          },
          [
            h("span", [
              h("Icon", {
                props: {
                  type:
                    data.children && data.children.length > 0
                      ? "md-albums"
                      : "ios-paper-outline"
                },
                style: {
                  marginRight: "5px",
                  display: "none"
                }
              }),
              h("span", data.title)
            ]),
            h(
              "span",
              {
                style: {
                  float: "right",
                  top: "6px",
                  display: "none"
                },
                on: {
                  mouseleave: e => {
                    e.stopPropagation();
                    //this.hideTools(event);
                  },
                },
                domProps: {
                  className: "tools"
                }
              },
              [
                h(
                  "Icon",
                  {
                    props: {
                      type: "md-add",
                      title: "添加"
                    },
                    on: {
                      click: e => {
                        e.stopPropagation();
                        this.appendClick(data);
                      }
                    }
                  },
                ),
                h(
                  "Icon",
                  {
                    props: {
                      type: "md-remove"
                    },
                    on: {
                      click: e => {
                        e.stopPropagation();
                        this.remove(root, node, data);
                      }
                    }
                  },
                ),
                h(
                  "Icon",
                  {
                    props: {
                      type: "md-arrow-round-up"
                    },
                    on: {
                      click: e => {
                        e.stopPropagation();
                        this.moveUp(root, node, data);
                      }
                    }
                  },
                ),
                h(
                  "Icon",
                  {
                    props: {
                      type: "md-arrow-round-down"
                    },
                    on: {
                      click: e => {
                        e.stopPropagation();
                        this.moveDown(root, node, data);
                      }
                    }
                  },
                ),
                h(
                  "Icon",
                  {
                    props: {
                      type: "md-create"
                    },
                    on: {
                      click: e => {
                        e.stopPropagation();
                        this.editClick(node, data);
                      }
                    }
                  },
                )
              ]
            )
          ]
        );
      },
      // 添加节点
      append(data, value) {
        const children = data.children || [];
        let newChild = {
          title: value,
          order: children.length,
          pid: data.rowKey,
          children: [],
          expand: true,
          type: data.type,
          volumeCode: this.volumeCode,
          remark: "备注",
        }
        children.push(newChild);
        this.$set(data, "children", children);
        this.$api.SaveOrUpdateSingleNode(newChild).then(res => {
          this.$Message.success("添加成功");
          this.$api.FindSyllabusByVolumeCode({ VolumeCode: this.volumeCode, Status: 1 }).then(res => {
              this.treeData = res.result.data;
            })
          })
      },
      // 删除节点
      remove(root, node, data) {
        const parentKey = root.find(el => el === node).parent;
        const parent = root.find(el => el.nodeKey === parentKey).node;
        const index = parent.children.indexOf(data);
        parent.children.splice(index, 1);
        data.status = 0;
        this.$api.SaveOrUpdateSingleNode(data).then(res => {
          this.$Message.success("删除成功");
        })
      },
      // 点击编辑
      editClick(node, data) {
        this.currentNode = data;
        this.editFlag = true;
        this.editValue = "";
      },
      //添加节点 弹出输入框
      appendClick(data) {
        this.currentNode = data;
        this.modalFlag = true;
        this.inputValue = "";
      },
      // 确认添加节点
      handleAddNode() {
        if (this.inputValue != "") {
          this.append(this.currentNode, this.inputValue);
        } else {
          this.modalFlag = false;
        }
      },
      // 修改节点
      handleUpdateNode() {
        if (this.editValue != "") {
          this.$api.SaveOrUpdateSingleNode(this.currentNode).then(res => {
              this.$Message.success("修改成功");
              this.currentNode.title = this.editValue;
            })
        } else {
          this.editFlag = false;
        }
      },
      // 标题点击收缩展开
      titleClick(root, node, data, event) {
        data.expand = !data.expand;
      },

      // 根目录点击事件
      rootClick(data) {
        data.expand = !data.expand;
      },
      // 上移章节操作
      moveUp(root, node, data) {
        const parentKey = root.find(el => el === node).parent;
        const parent = root.find(el => el.nodeKey === parentKey).node;
        const index = parent.children.indexOf(data);
        const nodeIndex = root.indexOf(node);
        let currentTitle = data.title;
        let currentExpand = data.expand;
        let currentChildren = data.children || [];
        let preChildren = (parent.children[index - 1]) ? (parent.children[index - 1]).children : [];
        if (index != 0) {
          data.title = parent.children[index - 1].title;
          parent.children[index - 1].title = currentTitle;
          parent.children[index - 1].expand = currentExpand;
          data.children = preChildren;
          data.expand = parent.children[index - 1].expand;
          parent.children[index - 1].children = currentChildren;
        }
      },
      // 下移章节操作
      moveDown(root, node, data) {
        const parentKey = root.find(el => el === node).parent;
        const parent = root.find(el => el.nodeKey === parentKey).node;
        const index = parent.children.indexOf(data);
        let currentTitle = data.title;
        let currentChildren = data.children || [];
        let nextChildren = (parent.children[index + 1]) ? (parent.children[index + 1]).children : [];
        if (index != parent.children.length - 1) {
          data.title = parent.children[index + 1].title;
          data.children = nextChildren;
          parent.children[index + 1].title = currentTitle;
          parent.children[index + 1].children = currentChildren;
        }
      },
      // 更多操作
      handleTools(root, node, data, event) {
        let toolsDom = event.currentTarget.nextElementSibling;
        const parentKey = root.find(el => el === node).parent;
        const parent = root.find(el => el.nodeKey === parentKey).node;
        const index = parent.children.indexOf(data);
        let list = document.getElementsByClassName("tools");
        let cFlag = toolsDom.style.display;
        for (let i = 0; i < list.length; i++) {
          list[i].style.display = "none";
        }
        // 判断TOOL显示与隐藏
        if (cFlag == "none") {
          toolsDom.style.display = "inline-flex";
          toolsDom.classList.add("animated");
          toolsDom.classList.add("slideInDown");
        } else {
          toolsDom.style.display = "none";
        }
      },
      // 鼠标从工具条移开的时候隐藏
      hideTools(event) {
        event.currentTarget.style.display = "none";
      },
      handleLiOver(event) {
        event.currentTarget.lastElementChild.style.display = "block";
        event.currentTarget.style.backgroundColor = "rgba(255, 255, 255, 0.68)";
        event.currentTarget.style.border = "1px solid #000";
      },
      handleLiLeave(node, event) {
        event.currentTarget.lastElementChild.style.display = "none";
        if (this.currentLiNode.nodeKey != node.nodeKey) {
          event.currentTarget.style.backgroundColor = "transparent";
          event.currentTarget.style.border = "0";
        }
      },
      // 添加教材
      handleAddBook() {
        this.$api.FindPeriods({}).then(res => {
          this.periodList = res.result.data;
          this.periodSelect = this.periodList[0].rowKey;
          this.periodName = this.periodList[0].name;
          let Pcode = {
            value: this.periodList[0].rowKey,
            label: this.periodList[0].name
          }
          this.periodChange(Pcode);
        })
        this.addBookFlag = true;
      },

      // 学段选择变化
      periodChange(val) {
        this.$api.FindSubjects({ Pcode: val.value }).then(res => {
          if (res.result.data.length > 0) {
            this.subjectList = res.result.data;
            this.subjectSelect = this.subjectList[0].rowKey;
            this.periodName = val.label;
            let Pcode = {
              value: this.subjectList[0].rowKey,
              label: this.subjectList[0].name
            }
            this.subjectChange(Pcode);
          }
        })
      },
      // 学科选择变化
      subjectChange(val) {
        this.$api.FindEditions({ Pscode: val.value }).then(res => {
          if (res.result.data.length > 0) {
            this.versionList = res.result.data;
            this.versionSelect = this.versionList[0].rowKey;
            this.subjectName = val.label;
            let Pcode = {
              value: this.versionList[0].rowKey,
              label: this.versionList[0].name
            }
            this.versionChange(Pcode);
          }
        })
      },
      // 版本选择变化
      versionChange(val) {
        this.$api.FindTerms({ Psecode: val.value }).then(res => {
          this.gradeList = res.result.data;
          this.gradeSelect = this.gradeList.length > 0 ? this.gradeList[0].rowKey : "无";
          this.versionName = val.label;
          let Pcode = {
            value: this.gradeList.length > 0 ? this.gradeList[0].rowKey : "",
            label: this.gradeList.length > 0 ? this.gradeList[0].name : ""
          }
          if (this.gradeList.length > 0) {
            this.gradeChange(Pcode);
          }
        })
      },
      //册别选择变化
      gradeChange(val) {
        this.gradeName = val.label ? val.label : "";
      },
      // 确认添加教材返回教材名称
      handleAddConfirm() {
        let periodRowKeys = [];
        let perioArr = this.periodList;
        for (let item of perioArr) {
          periodRowKeys.push(item.rowKey);
        }
        let bookName = this.periodName + this.subjectName + this.versionName + this.gradeName;
        this.treeData[0].title = bookName;
      }
    },
  };
</script>
<style scoped>
  .tree-main {
    margin-left: 5%;
    width: 90%;
  }

    .tree-main /deep/ .ivu-icon-ios-paper-outline {
    font-size: 16px;
  }

    .tree-main /deep/ .ivu-select {
    width: 80% !important;
  }

  .slideSelect .ivu-select-selection {
    height: 40px;
    background: #d8d8d870;
    border: 1px solid #808080;
  }

  .slideSelect, .slideSelect .ivu-icon {
    color: rgba(255,255,255,.8);
  }

    .tree-main /deep/ .ivu-select-single .ivu-select-selection .ivu-select-selected-value {
    height: 40px;
    line-height: 40px;
    font-size: 14px;
  }
  /* .ivu-tree ul li {
    margin: 18px 0;
  } */
  .tree-main /deep/ .ivu-tree ul {
    font-size: 14px;
  }

  .tree-main /deep/ .ivu-tree ul li {
    margin:8px 0;
  }

  .ivu-tree .ivu-tree-arrow {
    width: 2%;
  }

  .ivu-input-wrapper {
    width: 80% !important;
  }

  .modelRow {
    margin: 20px;
    font-size: 14px;
  }

  .tree-main /deep/ .tools {
    position: absolute;
    right: 60px;
    bottom:5px;
    display: inline-flex;
    z-index: 999;
  }

  .tree-main /deep/ .tools .ivu-icon {
    margin-right: 15px;
    font-size: 16px;
    font-weight: 200;
    color: #808080;
  }

  .btn_more {
    background: #fff;
    border: 1px solid rgb(248,248,248);
  }

  .animated {
    animation-duration: 0.5s;
  }

  @-webkit-keyframes slideInDown {
    from {
      -webkit-transform: translate3d(0,-10%, 0);
      transform: translate3d(0, -10%, 0) !important;
      visibility: visible;
    }

    to {
      -webkit-transform: translate3d(0, 0%, 0);
      transform: translate3d(0, 0%, 0);
    }
  }

  @keyframes slideInDown {
    from {
      -webkit-transform: translate3d(0, -10%, 0);
      transform: translate3d(0, -10%, 0) !important;
      visibility: visible;
    }

    to {
      -webkit-transform: translate3d(0, 0%, 0);
      transform: translate3d(0, 0%, 0);
    }
  }

  .slideInDown {
    -webkit-animation-name: slideInDown;
    animation-name: slideInDown;
  }

  .tree-main /deep/ .singleClass {
    display: inline-flex !important;
    flex-direction: row;
    align-items: center;
    height: 40px;
  }

  .btn-addClass {
    width: 16%;
    padding: 10px 6px !important;
  }

  .ivu-tabs-nav-scroll {
    display: flex;
    justify-content: center;
  }

  .addBookArea .ivu-select {
    width: 80% !important;
    margin-left: 30px;
  }

  .addBookArea .ivu-select-single .ivu-select-selection {
    height: 32px;
  }

  .addBookArea .ivu-row {
    margin: 30px 20px;
  }

  .addBookArea .ivu-select-selected-value {
    height: 32px !important;
    line-height: 32px !important;
    font-size: 12px !important;
  }

  .ivu-tabs {
    overflow: unset;
  }
</style>
