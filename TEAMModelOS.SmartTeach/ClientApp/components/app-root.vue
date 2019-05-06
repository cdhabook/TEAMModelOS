<template>
    <div id="app" class="container-fluid">
        <router-view/>
    </div>
</template>

<script>
    import NavMenu from './nav-menu'

    export default {
      components: {
        'nav-menu': NavMenu
      },

      data () {
        return {}
      }
    }
</script>

<style>
#app, body, html{
  width: 100%;
  height: 100%;
  font-family: 'Roboto', -apple-system, BlinkMacSystemFont, 'Segoe UI','Microsoft JhengHei';
}
</style>
