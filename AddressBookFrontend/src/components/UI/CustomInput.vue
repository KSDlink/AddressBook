<template>
  <input :value='modelValue' @input='updateInput'>
</template>

<script>
export default {
    name: 'custom-input',
    props: {
        modelValue: String
    },
    methods: {
        updateInput(event) {
            this.$emit('update:modelValue', event.target.value)
        }
    }
}
</script>

<style scoped>

</style>