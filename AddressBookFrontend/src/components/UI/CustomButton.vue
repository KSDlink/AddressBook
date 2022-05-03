<template>
  <button>
      <slot></slot>
  </button>
</template>

<script>
import { defineComponent } from 'vue';

export default defineComponent ({
    name: 'custom-button',
});
</script>

<style scoped>
  
</style>