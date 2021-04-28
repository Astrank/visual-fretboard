<template>
    <div class="fretboard">
        <div class="fretboard-selectors">
            <select v-model="scale.key" class="select-note" @change="createScale()">
                <option value="null" disabled selected hidden>Note</option>
                <option v-for="n in notes" 
                    :key="n" 
                    :value="n">
                        {{n}}
                </option>
            </select>

            <select v-model="scale.name" class="select-scale" @change="createScale()">
                <option value="null" disabled selected hidden>Scale</option>
                <option v-for="s in scales" 
                    :key="s" 
                    :value="s.name">
                        {{s.name}}
                </option>
            </select>
        </div>

        <div class="actual-fretboard"> 
            <div class="string" 
                v-for="string,index in fretboard.strings" 
                :key="string">
                <div v-for="note, indx in string" 
                    :key="note" 
                    :class="getFretClass(index, indx)">
                    <div :class="fretboard.notes[note].class" 
                        :style="{visibility: scale.notes != null && scale.notes.includes(note) ? 'visible' : 'hidden'}">
                        <p class="note-label" :style="{visibility: fretboard.notes[note].visible ? 'visible' : 'hidden'}">{{note}}</p>
                    </div>
                </div>
            </div>
        </div>

        <div class="degrees-container">
            <button v-for="d in scale.degrees" 
                :key="d" 
                :class="d.class" 
                :title="d.name" 
                @click="clickDegree(d.id)">
                    {{d.note != null ? d.note : d.symbol}}
            </button>
        </div>
    </div>

</template>

<script>

export default {
    name: 'Fretboard',
    data() {
        return {
            /* MUSIC THEORY */
            notes: ["C", "C#", "D", "D#", "E", "F","F#", "G", "G#", "A", "A#", "B"],
            scales: {
                Major: {
                    name: "Major",
                    pattern: [2, 2, 1, 2, 2, 2]
                },
                Dorian: {
                    name: "Dorian",
                    pattern: [2, 1, 2, 2, 2, 1]
                },
                Phrygian: {
                    name: "Prygian",
                    pattern: [1, 2, 2, 2, 1, 2]
                },
                Lydian: {
                    name: "Lydian",
                    pattern: [2, 2, 2, 1, 2, 2],
                },
                Mixolydian: {
                    name: "Mixolydian",
                    pattern: [2, 2, 1, 2, 2, 1],
                },
                Minor: {
                    name: "Minor",
                    pattern: [2, 1, 2, 2, 1, 2],
                },
                Locrian: {
                    name: "Locrian",
                    pattern: [1, 2, 2, 1, 2, 2]
                }
            },

            /* USER SCALE */
            scale: {
                key: null,
                name: null,
                pattern: null,
                notes: null,
                degrees: {
                    0: {
                        id: 0,
                        symbol: "I",
                        name: "Tonic",
                        note: null,
                        class: "degree"
                    },
                    1: {
                        id: 1,
                        symbol: "II",
                        name: "Supertonic",
                        note: null,
                        class: "degree"
                    },
                    2: {
                        id: 2,
                        symbol: "III",
                        name: "Mediant",
                        note: null,
                        class: "degree"
                    },
                    3: {
                        id: 3,
                        symbol: "IV",
                        name: "Subdominant",
                        note: null,
                        class: "degree"
                    },
                    4: {
                        id: 4,
                        symbol: "V",
                        name: "Dominant",
                        note: null,
                        class: "degree"
                    },
                    5: {
                        id: 5,
                        symbol: "VI",
                        name: "Submediant",
                        note: null,
                        class: "degree"
                    },
                    6: {
                        id: 6,
                        symbol: "VII",
                        name: "Leading",
                        note: null,
                        class: "degree"
                    }
                },
                test: null
            },

            fretboard: {
                frets: 17,
                notes: {
                    "C": {
                        name: "C",
                        class: "note C",
                        visible: false
                    },
                    "C#": {
                        name: "C#",
                        class: "note C#",
                        visible: false
                    },
                    "D": {
                        name: "D",
                        class: "note D",
                        visible: false
                    },
                    "D#": {
                        name: "D#",
                        class: "note D#",
                        visible: false
                    },
                    "E": {
                        name: "E",
                        class: "note E",
                        visible: false
                    },
                    "F": {
                        name: "F",
                        class: "note F",
                        visible: false
                    },
                    "F#": {
                        name: "F#",
                        class: "note F#",
                        visible: false
                    },
                    "G": {
                        name: "G",
                        class: "note G",
                        visible: false
                    },
                    "G#": {
                        name: "G#",
                        class: "note G#",
                        visible: false
                    },
                    "A": {
                        name: "A",
                        class: "note A",
                        visible: false
                    },
                    "A#": {
                        name: "A#",
                        class: "note A#",
                        visible: false
                    },
                    "B": {
                        name: "B",
                        class: "note B",
                        visible: false
                    }
                },

                strings: {
                    1: null,
                    2: null,
                    3: null,
                    4: null,
                    5: null,
                    6: null
                },
            }
        }
    },
    mounted() {
        this.fretboard.strings[1] = this.getString("E");
        this.fretboard.strings[2] = this.getString("B");
        this.fretboard.strings[3] = this.getString("G");
        this.fretboard.strings[4] = this.getString("D");
        this.fretboard.strings[5] = this.getString("A");
        this.fretboard.strings[6] = this.getString("E");
    },
    methods: {
        createScale() {
            if (this.scale.key != null && this.scale.name != null) {
                this.scale.pattern = this.scales[this.scale.name].pattern;
                this.scale.notes = this.getNotes();
                this.scale.chords = this.getChords();
            }
        },

        getNotes() {
            var keyIndex = this.notes.indexOf(this.scale.key);
            var result = [this.notes[keyIndex]];

            for (let i = 0; i < (this.scale.pattern.length); i++) {
                keyIndex += this.scale.pattern[i];
                if(keyIndex > 11){
                    keyIndex -= 12;
                }
                result.push(this.notes[keyIndex]);
            }

            return result;
        },

        getChords() {
            var result = [];

            for (let i = 0; i < this.scale.notes.length; i++) {
                var chord = [];
                chord.push(this.scale.notes[i]);

                var index = i;
                for(let y = 0; y < 2; y++){
                    index += 2;
                    if(index > 6){
                        index -= 7;
                    }
                    chord.push(this.scale.notes[index]);
                }
                var firstThird = this.thirdDetector(this.notes.indexOf(chord[0]), this.notes.indexOf(chord[1]));
                var secondThird = this.thirdDetector(this.notes.indexOf(chord[1]), this.notes.indexOf(chord[2]));
                
                if (firstThird == "minor" && secondThird == "minor") {
                    this.scale.degrees[i].note = (chord[0] + "dim");
                }
                else if (firstThird == "minor" && secondThird == "major") {
                    this.scale.degrees[i].note = (chord[0].toLowerCase());
                }
                else if (firstThird == "major" && secondThird == "minor") {
                    this.scale.degrees[i].note = (chord[0]);
                }
                else if (firstThird == "major" && secondThird == "major") {
                    this.scale.degrees[i].note = (chord[0] + "aug");
                }
            }

            return result;
        },

        getString(note){
            var index = this.notes.indexOf(note);
            var result = [this.notes[index]];

            for (let i = 1; i < this.fretboard.frets; i++) {
                index += 1;

                if (index > 11) {
                    index -= 12;
                }
                result.push(this.notes[index]);
            }

            return result;
        },

        thirdDetector(tonic, third){
            if (third < tonic) {
                third += 12;
            }
            if(third - tonic == 3){
                return "minor";
            } else if (third - tonic == 4){
                return "major";
            }
        },

        clickDegree(id) {
            if (this.scale.degrees[id].class == "degree") {
                this.scale.degrees[id].class = "degree " + this.scale.degrees[id].name
            } 
            else {
                this.scale.degrees[id].class = "degree"
            }

            var fNote = this.fretboard.notes[this.scale.notes[id]];
            
            if (fNote.class.includes(this.scale.degrees[id].name)) {
                fNote.class = "note " + this.scale.notes[id].name;
                fNote.visible = false;
            }
            else {
                fNote.class += " " + this.scale.degrees[id].name; 
                fNote.visible = true;
            }
        },

        getFretClass(stringIndex, fretIndex) {
            var result = "fret";

            if (fretIndex == 0) {
                result += " zero";
            }
            else if (fretIndex == 1) {
                result += " first";
            }
            else if(fretIndex == this.fretboard.frets - 1) {
                result += " last";
            }
            
            if (stringIndex == 6) {
                result += " last-string"
            }

            return result;
        }
    }
}
</script>