<template>
        <div class="metronome">
            <h1>METRONOME</h1>
            <input id="tempo" class="metronome-bpm" type="number" value="60" min="20" max="180" onchange="metronomeApp.setTempo(this.value);">
            <button class="metronome-signature">4/4</button>
            <input id="metronome" class="metronome-timer" type="button" value="Start" onclick="metronomeApp.toggle()">
        </div>
</template>

<script>
export default {
    name: "Metronome",
    data() {
        return {

        }
    }
}
</script>

<style>
.metronome {
  display: flex;
  flex-direction: row;
  background-color: rgb(165, 165, 165);
  color: #fff;
  max-width: 800px;
  margin: auto;
  margin-top: 5.5rem;
  padding: 1.4rem;
  -webkit-box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.2);
  -moz-box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.2);
  box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.2);
}

.metronome h1 {
  font-weight: 500;
  font-size: 1.8rem;
}

.metronome-header .header-timer {
  display: flex;
  margin-left: auto;
  border-radius: 50%;
  width: 63px;
  height: 63px;
  background-color: rgb(190, 190, 190);
  align-items: center;
  justify-content: center;
  border: 1px solid rgb(163, 161, 161);
  -webkit-box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.1);
  -moz-box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.1);
  box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.1);
  color: #fff;
  cursor: pointer;
}

.metronome-bpm,
.metronome-signature {
  margin-left: auto;
  padding: 7px 10px;
  font-size: 17px;
  background-color: rgb(190, 190, 190);
  color: #fff;
  border-radius: 2px;
  -webkit-box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.1);
  -moz-box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.1);
  box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.1);
  border: none;
  max-width: 50px;
}
</style>