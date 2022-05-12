<template>
  <select v-model='modelValue' @change='changeOption'>
      <option value='' hidden disabled selected>Выберете из списка</option>
      <option v-for='option in props.options' :key='option.value' :value='option.value'>{{ option.name }}</option>
  </select>
</template>


<script lang="ts">
export default {
    name: 'custom-select'
}
</script>

<script setup lang="ts">
import { ref, defineProps, defineEmits } from 'vue';
const props = defineProps({
    options: {
        type: Array,
        default: () => []
    }
})

const modelValue = ref('');

const emit = defineEmits(['update:modelValue']);

const changeOption = (event: any) => {
    emit('update:modelValue', event.target.value);
}
</script>

<style scpoed>

</style>