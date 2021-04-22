<template>
    <div class="spotify">
        <div class="spotify-search">
            <input v-model="input" class="spotify-search-input">
            <button class="spotify-search-btn" @click="getTracks()">
                <i class="fas fa-search"></i>
            </button>
        </div>
        <div v-for="t in tracks" class="track" :key="t.id">
            <img class="track-img" :src="t.imageUrl" alt="">
            <div class="track-data">
                <p class="track-name">{{t.name}}</p>
                <p class="track-artist">{{t.artist}} - {{t.album}}</p>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    name: 'Spotify',
    data() {
        return {
            tracks: [],
            input: ""
        };
    },
    methods: {
        getTracks() {
            axios.get("https://localhost:5001/spotify/search?input=" + this.input)
                .then(res => {
                    this.tracks= res.data;
                    console.log(res.data[0].imageUrl);
                });
        }
    }
}
</script>

<style>
.spotify {
    background-color: var(--grey-color);
    width: 25rem;
    display: flex;
    flex-direction: column;
}

.spotify-search {
    margin: 2rem;
    display: flex;
    align-items: center;
    position: relative;
}

.spotify-search-input {
    border: none;
    border-radius: 20px;
    padding: 0.5rem 1rem;
    width: 100%;
}

.spotify-search-btn {
    position: absolute;
    right: 0;
    margin-right: 0.3rem;
    color: var(--grey-color);
    border: none;
    background-color: transparent;
    cursor: pointer;
    font-size: 1rem;
}

.track {
    height: 5rem;
    background-color: var(--grey-color);
    display: flex;
    flex-direction: row;
    align-items: center;
    cursor: pointer;
}

.track:hover {
    background-color: rgb(145, 145, 145);
}

.track-img {
    height: 100%;
    padding: 3px 6px;
}

.track-data {
    display: flex;
    flex-direction: column;
}

.track-name {
    font-size: 1.1rem;
    margin-bottom: 0.3rem;
}

.track-artist {
    font-size: 0.8rem;
    color: rgb(68, 68, 68);
}
</style>