<template>
  <div class="main-wrapper">
    <div class="main">
      <header class="nav">
        <h1 class="logo">Visual Fretboard</h1>
      </header>
      <Fretboard/>
      <Metronome/>
    </div>
    <Spotify/>
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
  height: 100vh;
}

.main {
  height: 100%;
  width: 100%;
}
</style>
