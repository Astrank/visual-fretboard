<template>
        <div class="metronome">
            <h1>METRONOME</h1>
            <div class="metronome-bpm-controlls">
              <a class="metronome-minus" v-on:click="bpm--">
                <i class="fas fa-minus-circle"></i>
              </a>
              <input id="tempo" class="metronome-range" type="range" v-model="bpm" min="20" max="300" onchange="metronomeApp.setTempo(this.value);">
              <a class="metronome-plus" v-on:click="bpm++">
                <i class="fas fa-plus-circle"></i>
              </a>
              <input class="metronome-bpm" v-model="bpm" type="number">
            </div>
            <a href="" class="metronome-play">
              <i class="far fa-play-circle"></i>
            </a>
            <!--<input id="metronome" class="metronome-timer" type="button" value="Start" onclick="metronomeApp.toggle()">-->
        </div>
</template>

<script>
export default {
    name: "Metronome",
    data() {
        return {
          bpm: 60,
        }
    },
}
</script>

<style>
.metronome {
  display: flex;
  flex-direction: row;
  background-color: var(--grey-color);
  color: var(--light-color);
  max-width: 800px;
  margin: auto;
  margin-top: 6.5rem;
  padding: 1.3rem 2rem;
  -webkit-box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.2);
  -moz-box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.2);
  box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.2);
  border-radius: 2px;
  align-items: center;
  justify-content: center;
}

.metronome h1 {
  font-weight: 500;
  font-size: 1.8rem;
}

.metronome-bpm-controlls {
  display: flex;
  flex-direction: row;
  align-items: center;
  margin: auto;
}

.metronome-range {
  width: 13rem;
}

.metronome-minus,
.metronome-plus {
  margin-inline: 0.5rem;
  cursor: pointer;
}

.metronome-bpm {
  width: 3rem;
  text-align: center;
  padding: 0.4rem 0;
  border: none;
  border-radius: 3px;
  margin-left: 1.5rem;
  color: var(--grey-color);
  font-size: 1.2rem;
  outline: none;
}

.metronome-bpm::-webkit-outer-spin-button,
.metronome-bpm::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}
.metronome-bpm[type=number] {
  -moz-appearance: textfield;
}

.metronome-play {
  font-size: 3rem;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>