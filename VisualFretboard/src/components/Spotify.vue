<template>
    <div class="spotify">
        <div class="spotify-search">
            <input v-model="input" class="spotify-search-input" placeholder="Search songs...">
            <button class="spotify-search-btn" @click="getTracks()">
                <i class="fas fa-search"></i>
            </button>
        </div>
        <div class="tracks">
            <div v-for="t in tracks" class="track" :key="t.id" @click="getTrack(t.id)">
                <img class="track-img" :src="t.imageUrl" alt="">
                <div class="track-data">
                    <p class="track-name">{{t.name}}</p>
                    <p class="track-artist">{{t.artist}} - {{t.album}}</p>
                </div>
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
            input: "",
            tracks: [],
            track: [],
        };
    },
    mounted() {
        this.getPlaylist();
    },
    methods: {
        getTracks() {
            axios.get("https://localhost:5001/spotify/search?input=" + this.input)
                .then(res => {
                    this.tracks = res.data;
                });
        },
        getTrack(id) {
            axios.get("https://localhost:5001/spotify/features?id=" + id)
                .then(res => {
                    this.track = res.data;
                    console.log(this.track);
                });
        },
        getPlaylist() {
            axios.get("https://localhost:5001/spotify/playlists?id=37i9dQZF1DWXRqgorJj26U")
                .then(res => {
                    this.tracks = res.data;
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
    padding: 1.5rem;
    display: flex;
    align-items: center;
    position: relative;
}

.spotify-search-input {
    border: none;
    border-radius: 20px;
    padding: 0.5rem 1rem;
    width: 100%;
    outline: none;
    font-family: Roboto;
    -webkit-box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.2);
    -moz-box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.2);
    box-shadow: 3px 3px 9px -3px rgba(0,0,0,0.2);
}

.spotify-search-btn {
    position: absolute;
    right: 0;
    margin-right: 1.9rem;
    color: var(--grey-color);
    border: none;
    background-color: transparent;
    cursor: pointer;
    font-size: 1rem;
    outline: none;
    margin-bottom: 1px;
}

.tracks {
    position: stricky;
    overflow-y: scroll;
    overflow-x: hidden;
    scrollbar-color: rgb(87, 87, 87) var(--grey-color);
    scrollbar-width: thin;
    max-height: calc(100vh - 5rem);
}

.track {
    height: 5rem;
    background-color: var(--grey-color);
    display: flex;
    flex-direction: row;
    align-items: center;
    cursor: pointer;
    color: #fff;
}

.track:hover {
    background-color: rgb(70, 68, 68);
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
    color: rgb(117, 117, 117);
}
</style>