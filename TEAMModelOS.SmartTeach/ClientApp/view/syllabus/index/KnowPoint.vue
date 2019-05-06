
<template>
  <div class="know-main center">
    <div class="content-wrap">
      <div class="header-wrap">
        <Input placeholder="关键词搜索知识点" style="width:25% !important;" />
        <div class="list-type center">
          <Icon type="md-menu" size="20" :class="listType==0?'list-type-active':''" @click="changeListType(0)"/>
          <Icon type="ios-keypad" size="20" :class="listType==1?'list-type-active':''" @click="changeListType(1)"/>
        </div>
        <div class="header-right">
          <div class="header-right-item" @click="addPoint" v-show="listType==1">
            <Icon type="logo-buffer" size="18" />
            <span>新增知识点</span>
          </div>
          <div class="header-right-item" @click="composeBlock" v-show="listType==1 && pointCheckedList.length > 0">
            <Icon type="logo-buffer" size="18" />
            <span>组成知识块</span>
          </div>
          <div class="header-right-item" @click="addBlock" v-show="listType==0">
            <Icon type="ios-browsers" size="18" />
            <span>新增知识块</span>
          </div>
          <div class="header-right-item">
            <Icon type="ios-cloud-download" size="18" />
            <span>导入知识点</span>
          </div>
        </div>
      </div>
      <div class="sy-list-wrap">
        <!-- 知识块 -->
        <div class="list-col" v-if="isShowBlock">
          <Collapse simple accordion @on-change="collapseChange">
            <Panel v-for="(item,index) in knowBlockList" class="know-block-item" :key="index" :name="item.period">
              <Input v-if="item.id==block_edit_id" :ref="'block'+item.id" clearable type="text" v-model="item.name"
                     @on-blur="editKnowBlock(item)"
                     @on-enter="editKnowBlock(item)"
                     @click.stop.native="inputFocus($event)" />
              <span class="know-block-name" v-else>{{item.name}} | {{item.id}}</span>

              <span class="know-block-builder">建立者 | {{item.builder}}</span>
              <span class="k-block-tools">
                <Icon type="md-create" @click="handleEditBlock(item,$event)" title="编辑名称" />
                <Icon type="md-add-circle" title="编辑知识点" @click.stop.native="editBlockPoint(item)"/>
                <Icon type="md-trash" @click="handleDeleteBlock" title="删除"/>
              </span>
              <div slot="content" class="knowpoints">
                <div class="knowpoint-item" v-for="(item,index) in knowPointList" @mouseover="knowMouseover" @mouseout="knowMouseout">
                  <Input v-if="item.id==point_edit_id" :ref="'point'+item.blockId+item.id" clearable type="text" v-model="item.name"
                         @on-blur="editKnowPoint(item)"
                         @on-enter="editKnowPoint(item)"
                         @click.stop.native="inputFocus($event)" />
                  <span v-else>{{item.name}} </span>
                  <span class="k-tools">
                    <Icon type="md-create" @click="handleEditPoint(item,$event)" />
                    <Icon type="md-trash" @click.stop.native="handleDeleteBlock" />
                  </span>
                </div>
              </div>
            </Panel>
          </Collapse>
        </div>

        <!-- 知识点仓库 -->
        <div class="list-col list-knowpoints" v-else>
          <div class="knowpoints-filter">
            <span :class="filter_index == 0 ? 'sort-active':''" @click="filterSort(0)">综合排序</span>
            <span :class="filter_index == 1 ? 'sort-active':''" @click="filterSort(1)">名称排序</span>
            <span :class="filter_index == 2 ? 'sort-active':''" @click="filterSort(2)">创建时间排序</span>
          </div>
          <div class="knowpoints">
            <div v-for="(item,index) in knowPointList" :class="['knowpoint-item',pointCheckedList.indexOf(item) != -1?'knowpoint-item-active':'']" :title="item.description" @mouseover="knowMouseover" @mouseout="knowMouseout" @click="pointClick(item)">
              <Input v-if="item.id==point_edit_id" :ref="'point'+item.blockId+item.id" clearable type="text" v-model="item.name"
                     @on-blur="editKnowPoint(item)"
                     @on-enter="editKnowPoint(item)"
                     @click.stop.native="inputFocus($event)" />
              <span v-else class="overflow-text">{{item.name}} </span>
              <!--<Icon type="ios-checkmark-circle" color="#00fa00" class="icon-checked"/>-->
              <span class="k-tools">
                <Icon type="md-create" @click="handleEditPoint(item,$event)" />
                <Icon type="md-trash" @click.stop.native="handleDeleteBlock" />
              </span>
            </div>
          </div>
          <Page :current="2" :total="50" simple class="center" style="margin:20px 0"/>
        </div>

        <Modal v-model="addBlockModal"
               title="新增知识块"
               width="520"
               ok-text="确认"
               cancel-text="取消"
               @on-ok="handleAddBlock"
               @on-cancel="">
          <p class="modal-title">请输入知识块名称</p>
          <Input v-model="newBlockName" placeholder="输入名称" style="margin:5px" />
        </Modal>

        <Modal v-model="addPointModal"
               title="新增知识点"
               width="520"
               ok-text="确认"
               cancel-text="取消"
               @on-ok="handleAddPoint"
               @on-cancel="">
          <p class="modal-title">请输入知识点名称</p>
          <Input v-model="newPointName" placeholder="输入名称" style="margin:5px" />
          <p class="modal-title">请输入知识点描述</p>
          <Input v-model="newPointName" placeholder="输入描述" style="margin:5px" />
        </Modal>

        <Modal v-model="editBlockPointModal"
               title="编辑知识块"
               width="600"
               ok-text="确认"
               cancel-text="取消"
               @on-ok="handleAddBlock"
               @on-cancel="">
              <Transfer :data="data3"
                        :target-keys="targetKeys3"
                        :list-style="listStyle"
                        :render-format="render3"
                        :operations="['撤回知识库','加入知识块']"
                        filter-placeholder="请输入搜索内容"
                        :titles="['知识点仓库', '当前知识块']"
                        not-found-text="列表为空"
                        filterable
                        :filter-method="filterMethod"
                        @on-change="handleChange3">
                <div :style="{float: 'right', margin: '5px'}">
                  <Button size="small" @click="reloadMockData">刷新</Button>
                </div>
              </Transfer>
        </Modal>
        <!-- 组成知识块弹窗 -->
        <Modal v-model="composeBlockModal"
               title="组成知识块"
               width="400"
               ok-text="确认"
               cancel-text="取消"
               @on-ok="handleComposeBlock"
               @on-cancel="">
          <Tabs value="newBlock" class="compose-block-tab">
            <TabPane label="建立新的知识块" name="newBlock">
              <p class="modal-title">请输入知识块名称</p>
              <Input v-model="newComposeName" placeholder="请输入" style="margin:5px" />
              <p class="modal-title">当前选中知识点</p>
              <div class="checked-points">
                <div v-for="item in pointCheckedList" class="knowpoint-item knowpoint-item-active">{{item.name}}</div>
              </div>
            </TabPane>
            <TabPane label="移动到现有知识块" name="moveToBlock">
              <RadioGroup v-model="checkedBlock">
                <Radio v-for="(item,index) in knowBlockList" :key="index" :label="item.name+item.id" :value="item.period" :name="item.period"></Radio>
              </RadioGroup>
            </TabPane>
          </Tabs>
        </Modal>

      </div>
    </div>
  </div>
</template>

<script scoped>
  export default {
    name: "KnowPoint",
    data() {
      return {
        knowPointList: [],
        knowBlockList: [],
        pointCheckedList: [],
        checkedBlock:"知识块名称0",
        isShowBlock: false,
        listType: 1,
        filter_index:0,
        newBlockName:"",
        newPointName:"",
        newComposeName:"",
        block_edit_id: null,  //当前知识块编辑索引
        point_edit_id: null,  //当前知识点编辑索引
        point_click_id:null,
        addBlockModal: false, //新增知识块开关
        addPointModal: false, //新增知识点开关
        editBlockPointModal: false, //编辑知识块开关
        composeBlockModal: false, //编辑知识块开关
        data3: this.getMockData(),
        targetKeys3: this.getTargetKeys(),
        listStyle: {
          width: '220px',
          height: '420px'
        },
      }
    },
    created() {
      for (let i = 0; i < 11; i++) {
        this.knowPointList.push({
          id: i,
          key: i.toString(),
          label: i.toString(),
          name: "知识点名称1111_" + this.changeNumFormat(i),
          blockId: i + 1,
          description: "知识点名称1111_" + this.changeNumFormat(i)
        })

        this.knowBlockList.push({
          id: i,
          name: "知识块名称",
          period: i.toString(),
          builder: "郭金凤",
          userNum: i + 5
        })
      }
    },
    methods: {

      //知识点与知识块切换
      changeListType(status) {
        this.listType = status; 
        this.isShowBlock = status == 0;
      },
      //数字格式转换
      changeNumFormat(num) {
        let nums = num.toString();
        if (nums.length == 1) {
          nums = '00' + nums;
        } else if (nums.length == 2) {
          nums = '0' + nums;
        }
        return nums;
      },
      //鼠标滑入事件显示操作选项
      knowMouseover(e) {
        e.stopPropagation();
        e.currentTarget.lastElementChild.style.visibility = 'visible';
      },
      //鼠标滑出事件隐藏操作选项
      knowMouseout(e) {
        e.stopPropagation();
        e.currentTarget.lastElementChild.style.visibility = 'hidden';
      },
      //折叠版切换回调
      collapseChange(arr) {
        this.block_edit_id = null;
          let list = document.getElementsByClassName('k-block-tools');
          for (let i = 0 ; i < list.length ;i++) {
            if (arr.indexOf(i.toString()) != -1) {
              list[i].style.visibility = "visible";
            } else {
              list[i].style.visibility = "hidden";
            }
        }
      },

      filterSort(index) {
        this.filter_index = index;
      },

      //点击编辑知识块
      handleEditBlock(data, e) {
        e.stopPropagation();
        this.block_edit_id = data.id;
        this.$nextTick(() => {
          this.$refs['block' + data.id][0].focus();
        })
      },
      //编辑知识块input失焦
      editKnowBlock(data) {
        this.block_edit_id = null;
      },

      //点击编辑知识点
      handleEditPoint(data, e) {
        e.stopPropagation();
        this.point_edit_id = data.id;
        this.$nextTick(() => {
          this.$refs['point'+data.blockId+data.id][0].focus();
        })
      },
      //编辑知识点input失焦
      editKnowPoint(data) {
        this.point_edit_id = null;
      },

      //input聚焦
      inputFocus(e) {
        e.cancelBubble = true;
      },

      //request删除知识块
      handleDeleteBlock() {
        this.$Modal.confirm({
          title: '删除知识块',
          content: '<p>确认删除该知识块？</p>',
          okText:"确认",
          cancelText:"取消",
          onOk: () => {
            this.$Message.info('Clicked ok');
          },
          onCancel: () => {
            this.$Message.info('Clicked cancel');
          }
        });
      },
      //点击新增知识块弹窗
      addBlock() {
        this.addBlockModal = true;
      },
      //点击新增知识块弹窗
      addPoint() {
        this.addPointModal = true;
      },
      //点击组成知识块弹窗
      composeBlock() {
        if (this.pointCheckedList.length < 1) {
          this.$Message.info('请先勾选需要操作的知识点！');
        } else {
          this.composeBlockModal = true;
        }
      },
      //点击编辑知识块弹窗
      editBlockPoint(data) {
        let block = data;
        this.editBlockPointModal = true;
        this.reloadMockData();
      },
      //request新增知识块
      handleAddBlock() {
        console.log(this.newBlockName);
        this.newBlockName = "";
      },

      //request新增知识点
      handleAddPoint() {
        console.log(this.newPointName);
        this.newPointName = "";
      },
      //request编辑知识块
      handleEditBlockPoint() {
        console.log(this.targetKeys3);
        
      },

      //request编辑知识块
      handleComposeBlock() {
        console.log(this.pointCheckedList);
        console.log(this.newComposeName);
        this.newComposeName = ""
      },

      pointClick(data) {
        //this.point_click_id = data.id;
        let list = this.pointCheckedList;
        if (list.indexOf(data) == -1) {
          list.push(data);
        } else {
          list.splice(list.indexOf(data), 1);
        }
      },

      getMockData() {
        let mockData = [];
        for (let i = 1; i <= 20; i++) {
          mockData.push({
            id: i,
            key: i.toString(),
            label: i.toString(),
            name: "知识点名称_" + this.changeNumFormat(i),
            termNum: i + 5,
            blockId: i + 1,
            description: "知识点名称_" + this.changeNumFormat(i)
          });

        }
        return mockData;
      },
      getTargetKeys() {
        let targetArr = [];
        return targetArr;
      },
      handleChange3(newTargetKeys) {
        this.targetKeys3 = newTargetKeys;
      },
      render3(item) {
        return item.label + ' - ' + item.description;
      },
      //重置数据
      reloadMockData() {
        this.data3 = this.getMockData();
        this.targetKeys3 = this.getTargetKeys();
      },
      //transfer Input 筛选数据
      filterMethod(data, query) {
        let val = data.label + ' - ' + data.description;
        return val.toUpperCase().indexOf(query.toUpperCase()) > -1;
      }
    }
  }
</script>
