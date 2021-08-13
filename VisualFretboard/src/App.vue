<template>
  <div class="main-wrapper" v-show="!this.$store.state.loading">
    <div class="main">
      <header class="nav">
        <h1 class="logo">Visual Fretboard</h1>
      </header>
      <Fretboard/>
      <Metronome/>
    </div>
    <Spotify/>
  </div>
  <div class="loading" v-show="this.$store.state.loading">
    <i class="fas fa-guitar guitar"></i>
  </div>
</template>

<script>
import Spotify from './components/Spotify';
import Fretboard from './components/Fretboard';
import Metronome from './components/Metronome';

export default {
  name: 'App',
  components: {
    Spotify,
    Fretboard,
    Metronome
  },
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

.loading {
  width: 100vw;
  height: 100vh;
  display: flex;
}

.guitar {
  font-size: 4em;
  color: var(--light-color);
  margin: auto;
  align-self: center;
  justify-self: center;
  -webkit-animation:spin 4s linear infinite;
  -moz-animation:spin 4s linear infinite;
  animation:spin 4s linear infinite;
}

@-moz-keyframes spin { 100% { -moz-transform: rotate(360deg); } }
@-webkit-keyframes spin { 100% { -webkit-transform: rotate(360deg); } }
@keyframes spin { 100% { -webkit-transform: rotate(360deg); transform:rotate(360deg); } }
</style>
