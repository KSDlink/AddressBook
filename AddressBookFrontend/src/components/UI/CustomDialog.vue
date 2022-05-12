<template>
  <div class='dialog' v-if='props.show === true' @click.stop='hideDialog'>
    <div class='dialog__content' @click.stop>
        <slot></slot>
    </div>
  </div>
</template>

<script lang="ts">
export default {
    name: 'custom-dialog'
}
</script>


<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';
  const props = defineProps({ //СПРОСИ
    show: {
      type: Boolean,
      default: false,
    }
  });

  const emit = defineEmits(['update:show']);

  const hideDialog = () => {
    emit('update:show', false);
  }
</script>

<style scoped>
.dialog {
  top: 0;
  bottom: 0;
  right: 0;
  left: 0;
  background: rgba(0, 0, 0, 0.5);
  position: fixed;
  display: flex;
}
.dialog__content {
  margin: auto;
  background: white;
  border-radius: 12px;
  min-height: 50px;
  min-width: 300px;
  padding: 20px;
}

</style>