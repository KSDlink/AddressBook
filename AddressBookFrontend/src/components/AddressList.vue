<template>
<div class='my-3' v-if='contacts.length > 0'>
  <transition-group name='contact-list' tag='div'>
    <address-item
      v-for='contact in contacts'
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
import { defineComponent } from 'vue';
import AddressItem from '@/components/AddressItem.vue';
import {contactItem} from '@/types'


export default defineComponent ({
  components: { AddressItem },
  name: 'address-list',
  props: {
    contacts: {
      type: Array,
      required: true
    } 
  },
  methods: {
    removeContact(contact: contactItem){
      this.$emit('removeContact', contact);
    },
    updateContact(contact: contactItem){
      this.$emit('updateContact', contact);
    }
  }
});
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
