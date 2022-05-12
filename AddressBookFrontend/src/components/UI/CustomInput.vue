<template>
  <input :value='props.modelValue' @input='updateInput'>
</template>

<script lang="ts">
export default {
    name: 'custom-input'
}
</script>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';
const props = defineProps({
    modelValue: String,
})

const emit = defineEmits(['update:modelValue']);

const updateInput = (event: any) => {
    emit('update:modelValue', event.target.value);
}
</script>

<style scoped>

</style>