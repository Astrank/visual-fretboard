<template>
    <div class="fretboard">
        <div class="fretboard-selectors">
            <select v-model="scale.key" class="select-note" @change="createScale()">
                <option selected disabled hidden value="null">Note</option>
                <option v-for="n in notes" 
                    :key="n" 
                    :value="n">
                        {{n}}
                </option>
            </select>

            <select v-model="scale.name" class="select-scale" @change="createScale()">
                <option selected disabled hidden value="null">Scale</option>
                <option v-for="s in scales" 
                    :key="s" 
                    :value="s.name">
                        {{s.name}}
                </option>
            </select>
        </div>

        <div class="actual-fretboard"> 
            <div class="string" 
                v-for="string, stringIndex in fretboard.strings" :key="string">
                <div v-for="fret, fretIndex in fretboard.frets" 
                    :key="fret" 
                    :class="getFretClass(string, fretIndex)">
                    <div :class="fretboard.notes[getNote(stringIndex, fretIndex)].class" 
                        :style="{visibility: scale.notes != null && scale.notes.includes(fretboard.notes[getNote(stringIndex, fretIndex)].name) ? 'visible' : 'hidden'}">
                        <p class="note-label" 
                            v-show="fretboard.notes[getNote(stringIndex, fretIndex)].visible">
                                {{fretboard.notes[getNote(stringIndex, fretIndex)].name}}
                        </p>
                    </div>
                </div>
            </div>
        </div>


        <div class="degrees-container">
            <button v-for="degree, index in scale.degrees" 
                :key="degree" 
                :class="degree.class" 
                :title="degree.name" 
                @click="clickDegree(index)">
                    {{degree.note != null ? degree.note : degree.symbol}}
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
            notesTest: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
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
                degrees: [
                    {
                        id: 0,
                        symbol: "I",
                        name: "Tonic",
                        note: null,
                        class: "degree"
                    },
                    {
                        id: 1,
                        symbol: "II",
                        name: "Supertonic",
                        note: null,
                        class: "degree"
                    },
                    {
                        id: 2,
                        symbol: "III",
                        name: "Mediant",
                        note: null,
                        class: "degree"
                    },
                    {
                        id: 3,
                        symbol: "IV",
                        name: "Subdominant",
                        note: null,
                        class: "degree"
                    },
                    {
                        id: 4,
                        symbol: "V",
                        name: "Dominant",
                        note: null,
                        class: "degree"
                    },
                    {
                        id: 5,
                        symbol: "VI",
                        name: "Submediant",
                        note: null,
                        class: "degree"
                    },
                    {
                        id: 6,
                        symbol: "VII",
                        name: "Leading",
                        note: null,
                        class: "degree"
                    }
                ],
            },

            fretboard: {
                frets: 17,
                strings: 6,
                keyStrings: [4, 11, 7, 2, 9, 4],
                notes: [
                    {
                        name: "C",
                        class: "note C",
                        visible: false
                    },
                    {
                        name: "C#",
                        class: "note C#",
                        visible: false
                    },
                    {
                        name: "D",
                        class: "note D",
                        visible: false
                    },
                    {
                        name: "D#",
                        class: "note D#",
                        visible: false
                    },
                    {
                        name: "E",
                        class: "note E",
                        visible: false
                    },
                    {
                        name: "F",
                        class: "note F",
                        visible: false
                    },
                    {
                        name: "F#",
                        class: "note F#",
                        visible: false
                    },
                    {
                        name: "G",
                        class: "note G",
                        visible: false
                    },
                    {
                        name: "G#",
                        class: "note G#",
                        visible: false
                    },
                    {
                        name: "A",
                        class: "note A",
                        visible: false
                    },
                    {
                        name: "A#",
                        class: "note A#",
                        visible: false
                    },
                    {
                        name: "B",
                        class: "note B",
                        visible: false
                    }
                ],
            }
        }
    },
    methods: {
        createScale() {
            if (this.scale.key != null && this.scale.name != null) {
                this.scale.pattern = this.scales[this.scale.name].pattern;
                this.scale.notes = this.getNotes();
                this.scale.chords = this.getChords();
            }

            this.defaultLayout();
        },

        getNote(string,fret) {
            var result = this.fretboard.keyStrings[string] + fret;
            
            if (result > 23) {
                result -= 24;
            }
            else if (result > 11) {
                result -= 12;
            }

            return result;
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

        clickDegree(index) {
            if (this.scale.degrees[index].class == "degree") {
                this.scale.degrees[index].class = "degree " + this.scale.degrees[index].name.toLowerCase();
            } 
            else {
                this.scale.degrees[index].class = "degree"
            }

            var fNote = this.notes.indexOf(this.scale.notes[index]);

            
            if (this.fretboard.notes[fNote].class.includes(this.scale.degrees[index].name.toLowerCase())) {
                this.fretboard.notes[fNote].class = "note " + this.scale.notes[index].name;
                this.fretboard.notes[fNote].visible = false;
            }
            else {
                this.fretboard.notes[fNote].class += " " + this.scale.degrees[index].name.toLowerCase(); 
                this.fretboard.notes[fNote].visible = true;
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
        },

        defaultLayout() {
            var notes = this.fretboard.notes;
            for (let i = 0; i < notes.length; i++) {
                notes[i].class = `note ${notes[i].name}`;
                notes[i].visible = false; 
            }

            var degrees = this.scale.degrees;
            for (let i = 0; i < this.scale.degrees.length; i++) {
                degrees[i].class = "degree"
            }
        }
    }
}
</script>