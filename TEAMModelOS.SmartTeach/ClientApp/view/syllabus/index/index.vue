<style>
  @import 'Syllabus.css';
  @import 'KnowPoint.css';
  @import '../../../assets/CSS/syllabus/common.css';

</style>

<template>
  <div class="index">
     <div class="sak-header">
       <Headers :parentToChild="syllabusTitle" :identityselect="identitydata"></Headers>
     </div>
     <div class="sak-main center">
       <div class="sak-left">
         <Syllabus></Syllabus>
       </div>
       <div class="sak-right">
         <KnowPoint></KnowPoint>
       </div>
     </div>
  </div>
</template>
<script>
  import Headers from '@/common/headers.vue'
  import Syllabus from './Syllabus.vue'
  import KnowPoint from './KnowPoint.vue'
  export default {
    name: "app",
    components: {
      Headers: Headers,
      Syllabus: Syllabus,
      KnowPoint: KnowPoint
    },
    data() {
      return {
        syllabusTitle:"课纲管理/知识点管理",
        schoolName: "",
        username: "",
        activeIndex: sessionStorage.getItem('kIndex') || 0,
        identitydata: [
              { "id": 1, "name": '成都紫藤小学', "rolename": '管理员', status: '1' },
              { "id": 2, "name": '成都七中小学', "rolename": '班主任', status: '2' }
            ],
      }
    },
    created() {
    
    },
    methods: {

    },
    mounted() {
      
    }
  }
</script>
<style scoped>
  .index {
    position: relative;
    width: 100%;
    height:100%;
    overflow-y:hidden;
    min-width: 1366px;
    background: #fff;
    -moz-user-select: none; /*火狐 firefox*/
    -webkit-user-select: none; /*webkit浏览器*/
    -ms-user-select: none; /*IE10+*/
    user-select: none;
    display: flex;
    flex-direction: column;
    align-items: center;
  }
  .sak-header{
     width:100%;
     height:60px;
  }
  .sak-main {
    width: 100%;
    height: 100%;
  }

  .sak-left {
    width: 60%;
    height: 100%;
  }
  .sak-right {
    width: 40%; 
    height: 100%;
  }
  .center {
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
  }

  .centerCol {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }
</style>
