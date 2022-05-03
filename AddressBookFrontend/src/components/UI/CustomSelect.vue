<template>
  <select v-model='modelValue' @change='changeOption'>
      <option value='' hidden disabled selected>Выберете из списка</option>
      <option v-for='option in options' :key='option.value' :value='option.value'>{{ option.name }}</option>
  </select>
</template>

<script>
import { defineComponent } from 'vue';
export default defineComponent  ({
    name: 'custom-select',
    data(){
        return{
                modelValue: {
                type: String,
            },
        }
    },
    props: {
        options: {
            type: Array,
            default: () => []
        },
    },
    methods: {
        changeOption(event) {
            this.$emit('update:modelValue', event.target.value);
        }
    },
})
</script>

<style scpoed>

</style>