<template>
<div class='my-3' v-if='contacts.length > 0'>
  <transition-group name='contact-list' tag='div'>
    <address-item
      v-for='contact in props.contacts'
      :key='contact.id'
      :contact='contact'
      @removeContact='removeContact'
      @updateContact='updateContact'
    />
  </transition-group>
</div>
<div v-else>
  <h2>Нет добавленных контактов</h2>
</div>
  
</template>

<script lang="ts">
export default {
    name: 'address-list'
}
</script>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';
import {contactItem} from '@/types';
import AddressItem from '@/components/AddressItem.vue';

const props = defineProps({
  contacts: {
    type: Array,
    required: true,
  }
})

const emit = defineEmits(['removeContact', 'updateContact']);

const removeContact = (contact: contactItem) => {
  emit('removeContact', contact);
}

const updateContact = (contact: contactItem) => {
  emit('updateContact', contact);
}
</script>

<style scoped>
  .contact-list-move,
  .contact-list-enter-active,
  .contact-list-leave-active {
    transition: all 0.2s ease;
  }
  .contact-list-enter-from,
  .contact-list-leave-to {
    opacity: 0;
    transform: translateX(150px);
  }
</style>
