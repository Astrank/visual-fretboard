<template>
  <div class="main-wrapper">
    <div class="main"></div>
    <Spotify class="spotify-component" />
  </div>
</template>

<script>
import Spotify from './components/Spotify'

export default {
  name: 'App',
  components: {
    Spotify
  }
}
</script>

<style>
@import './assets/css/main.css';

.main-wrapper {
  display: flex;
  flex-direction: row;
  min-height: 100vh;
}

.main {
  height: 100%;
  width: 100%;
}
</style>
