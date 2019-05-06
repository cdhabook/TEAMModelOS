<template>
    <div>
       <h1>LOGIN</h1>
       <h2>{{ $t('test')}}</h2>
        <Card>
          <p slot="title">RESTful測試</p>
          <Button @click="ajax_TEST">測試按鈕8</Button>
          <pre>{{ testStr }}</pre>
        </Card>
    </div>
</template>

<script>
export default {
  data () {
    return {
      testStr: 'RESTful',
      testStr2: 'Json RPC',
    }
  },
  methods:{
    ajax_TEST: function(){
      this.$http.get('/api/values')
      .then(response => {
        this.testStr = response.data
      }).catch((error) => console.log(error))
    }
  }
}
</script>

<style>
</style>
